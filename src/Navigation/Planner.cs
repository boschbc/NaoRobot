using System.Collections.Generic;
using System.Drawing;
//using Naovigate.Util;

namespace Naovigate.Navigation
{
    class Planner
    {
        /// <summary>
        /// Plans the route. Takes as argument the map and a list of tiles, returns a planned list of RouteEntries.
        /// Returns null if no route could be planned.
        /// </summary>
        /// <returns>The route.</returns>
        /// <param name="map">Map to plan on.</param>
        /// <param name="points">Points to plan.</param>
        public static List<RouteEntry> PlanRoute(Map map, List<Point> points)
        {
            Point? pos = null;
            List<RouteEntry> route = new List<RouteEntry>();
            
            foreach (Point next in points)
            {
                // Set initial position.
                if (pos.HasValue)
                {
                    // Start planning.
                    Direction target = GetTargetDirection(pos, next);

                    // Get the tile and start searching.
                    Tile tile = map.TileAt(pos.Value.X, pos.Value.Y);
                    RouteEntry nextEntry = NextEntry(map, tile, target);

                    // route is not plannable
                    if (nextEntry == null) return null;

                    route.Add(nextEntry);
                }
                pos = next;
            }
            return Flatten(route);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tile"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static RouteEntry NextEntry(Map map, Tile tile, Direction target)
        {
            int distance = 0;
            while (true)
            {
                // Get next tile in direction.
                int x = tile.X + (target == Direction.Left ? -1 : target == Direction.Right ? 1 : 0);
                int y = tile.Y + (target == Direction.Up ? -1 : target == Direction.Down ? 1 : 0);

                // If we reached the outskirts and found no marker, this is an unplannable route.
                if (!map.WithinBorders(x, y))
                    return null;

                distance++;
                tile = map.TileAt(x, y);

                // Found a marker? Great. Plan it.
                if (tile.HasMarkerAt(target))
                {
                    return new RouteEntry(target, tile.MarkerAt(target), distance, distance - 1);
                }

                // If there is a wall but no marker, this is an unplannable route.
                if (tile.HasWallAt(target))
                    return null;
            }
        }

        /// <summary>
        /// Get direction to start looking in.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public static Direction GetTargetDirection(Point? pos, Point next)
        {
            Direction target;
            if (next.X < pos.Value.X)
                target = Direction.Left;
            else if (next.X > pos.Value.X)
                target = Direction.Right;
            else if (next.Y < pos.Value.Y)
                target = Direction.Up;
            else
                target = Direction.Down;
            return target;
        }

        /// <summary>
        /// Flatten the route.
        /// Removes any redundant RouteEntries.
        /// </summary>
        /// <param name="interRoute">The unflattened route.</param>
        /// <returns>A flattened route</returns>
        public static List<RouteEntry> Flatten(List<RouteEntry> interRoute)
        {
            List<RouteEntry> route = new List<RouteEntry>();
            foreach (RouteEntry entry in interRoute)
            {
                if (route.Count == 0)
                    route.Add(entry);
                if (entry.Direction != route[route.Count - 1].Direction)
                    route.Add(entry);
                else
                    route[route.Count - 1].WantedDistance = entry.WantedDistance;
            }
            return route;
        }
    }
}
