﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
//using Naovigate.Util;

namespace Naovigate.Navigation
{
    class Planner
    {
        public static List<RouteEntry> PlanRoute(Map map, List<Point> points)
        {
            Point? pos = null;
            List<RouteEntry> interRoute = new List<RouteEntry>();
            List<RouteEntry> route = new List<RouteEntry>();

            foreach (Point next in points)
            {
                // Set initial position.
                if (!pos.HasValue)
                {
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

                Console.WriteLine("Selected direction: {0}", Enum.GetName(typeof(Map.Direction), target));
                Map.Tile tile = map.TileAt(pos.Value.X, pos.Value.Y);
                int distance = 0;
                while (true)
                {
                    // Get next tile in direction.
                    int x = tile.X + (target == Map.Direction.Left ? -1 : target == Map.Direction.Right ? 1 : 0);
                    int y = tile.Y + (target == Map.Direction.Up ? -1 : target == Map.Direction.Down ? 1 : 0);

                    // If we reached the outskirts and found no marker, this is an unplannable route.
                    Console.WriteLine("Checking point ({0}, {1})... ({2}, {3})", x, y, map.WithinBorders(x, y), map.TileAt(x, y).HasMarkerAt(target));
                    if (!map.WithinBorders(x, y))
                        return null;

                    distance++;
                    tile = map.TileAt(x, y);

                    // Found a marker? Great. Plan it.
                    if (tile.HasMarkerAt(target))
                    {
                        interRoute.Add(new RouteEntry(target, tile.MarkerAt(target), distance, distance - 1));
                        break;
                    }

                    // If there is a wall but no marker, this is an unplannable route.
                    if (tile.HasWallAt(target))
                        return null;
                }
                pos = next;
            }

            // Flatten list.
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
