﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;
using Naovigate.Navigation;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// follow the given path, stopping on the last node.
    /// </summary>
    public class GoToEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.GoTo;

        private List<Point> locations;

        private MarkerSearchThread worker;
        
        /// <summary>
        /// Creates a new GoToEvent.
        /// </summary>
        public GoToEvent()
        {
            Unpack();
        }

        /// <summary>
        /// Creates a new GoToEvent using the given locations.
        /// </summary>
        /// <param name="locations">List of MarkerID's</param>
        public GoToEvent(ICollection<Point> locations)
        {
            if (locations == null)
                this.locations = new List<Point>();
            else
                this.locations = new List<Point>(locations);
        }
        
        /// <summary>
        /// Creates a new GoToEvent using the given locations.
        /// </summary>
        /// <param name="locations"></param>
        public GoToEvent(params Point[] locations) : this(new List<Point>(locations)) { }

        /// <summary>
        /// Extract the MarkerID and Distance parameters from the communication stream.
        /// </summary>
        private void Unpack()
        {
            int nodes = stream.ReadInt();
            locations = new List<Point>();
            for (int i = 0; i < nodes;i++ )
            {
                locations.Add(new Point(stream.ReadInt(), stream.ReadInt()));
            }
        }

        /// <summary>
        /// Fire this event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                List<RouteEntry> route = Planner.PlanRoute(NaoState.Instance.Map, locations);
                foreach (RouteEntry entry in route)
                {
                    if (!Aborted)
                    {
                        worker = Walk.Instance.WalkTowardsMarker((float)entry.Direction.ToRadian(), entry.MarkerID, entry.WantedDistance);
                        worker.WaitFor();
                    }
                }
                ReportSuccess();
            }
            catch
            {
                ReportFailure();
            }
        }

        /// <summary>
        /// Abort this event's execution.
        /// </summary>
        public override void Abort()
        {
            base.Abort();
            try
            {
                if (worker != null)
                    worker.Abort();
            }
            catch
            {
                ReportError();
            }
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.GoTo; }
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
 	        string s = base.ToString() + "<\n\t";
            foreach (Point p in locations)
                s += p.ToString() + ",\n\t";
            return s.Substring(0, s.Length - 1) + ">";
        }
        
    }
}
