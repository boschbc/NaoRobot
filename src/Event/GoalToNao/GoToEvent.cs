using System;
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
    /// Move to the marker MarkerID, if it is in range of the Nao.
    /// The Nao will stop Distance rooms away from the marker.
    /// </summary>
    public class GoToEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.GoTo;

        private List<Point> locations;

        private MarkerSearchThread worker;
        private bool aborted;

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
            NaoEvent statusEvent = new SuccessEvent(code);
            try
            {
                List<RouteEntry> route = Planner.PlanRoute(NaoState.Instance.Map, locations);
                foreach (RouteEntry entry in route)
                {
                    if (!aborted)
                    {
                        worker = Walk.Instance.WalkTowardsMarker((float)entry.Direction.ToRadian(), entry.MarkerID, entry.Distance);
                        worker.WaitFor();
                    }
                }
            }
            catch
            {
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /// <summary>
        /// Abort this event's execution.
        /// </summary>
        public override void Abort()
        {
            aborted = true;
            try
            {
                if (worker != null)
                    worker.Abort();
            }
            catch
            {
                EventQueue.Goal.Post(new ErrorEvent());
            }
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
