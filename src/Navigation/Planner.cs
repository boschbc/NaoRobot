using System;
using System.Collections.Generic;
using System.Drawing;
using Naovigate.Util;

namespace Naovigate.Navigation
{
    class Planner
    {
        public static List<RouteEntry> PlanRoute(Map map, List<Point> points)
        {
            Point? pos = null;
            List<RouteEntry> interRoute = new List<RouteEntry>();
            List<RouteEntry> route = new List<RouteEntry>();

            foreach (Point next in points) {
                // Set initial position.
                if (!pos.HasValue) {
                    pos = next;
                    continue;
                }

                // Start planning.
                Map.Direction target;
                // Get direction to start looking in.
                if (next.X < pos.Value.X)
                    target = Map.Direction.Left;
                else if (next.X > pos.Value.X)
                    target = Map.Direction.Right;
                else if (next.Y < pos.Value.Y)
                    target = Map.Direction.Up;
                else if (next.Y > pos.Value.Y)
                    target = Map.Direction.Down;
                else
                    continue;

                Map.Tile tile = map.TileAt(pos.Value.X, pos.Value.Y);
                int distance = 0;
                while (true) {
                    // Get next tile in direction.
                    int x = tile.X + (target == Map.Direction.Left ? -1 : target == Map.Direction.Right ? 1 : 0);
                    int y = tile.Y + (target == Map.Direction.Up ? -1 : target == Map.Direction.Down ? 1 : 0);

                    // If we reached the outskirts and found no marker, this is an unplannable route.
                    if (!map.WithinBorders(x, y))
                        return null;

                    distance++;
                    tile = map.TileAt(x, y);

                    // Found a marker? Great. Plan it.
                    if (tile.HasMarkerAt(target)) {
                        interRoute.Add(new RouteEntry(target, tile.MarkerAt(target), 1.0f / distance));
                        break;
                    }

                    // If there is a wall but no marker, this is an unplannable route.
                    if (tile.HasWallAt(target))
                        return null;
                }
            }

            // Flatten list.
            Map.Direction? prev = null;
            float dist = 0.0f;
            int markerID = 0;
            foreach (RouteEntry entry in interRoute) {
                if (prev.HasValue && prev.Value == entry.Direction) {
                    dist += entry.Distance;
                    continue;
                }

                if (dist > 0.0f)
                    route.Add(new RouteEntry(prev.Value, markerID, dist));
                prev = entry.Direction;
                dist = entry.Distance;
                markerID = entry.MarkerID;
            }

            return route;
        }
    }
}
