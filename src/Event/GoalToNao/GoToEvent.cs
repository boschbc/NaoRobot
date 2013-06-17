using System.Collections.Generic;
using System.Drawing;

using Naovigate.Event.Internal;
using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;
using Naovigate.Navigation;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// follow the given path, stopping on the last node.
    /// </summary>
    public sealed class GoToEvent : ReportBackEvent
    {
        private List<Point> locations;
        
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
            int nodes = Stream.ReadInt();
            locations = new List<Point>();
            for (int i = 0; i < nodes;i++ )
            {
                locations.Add(new Point((Stream.ReadInt() - 5) / 10, (Stream.ReadInt() - 5) / 10));
            }
        }

        /// <summary>
        /// Fire this event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                Logger.Log(this, "Planning route...");
                List<RouteEntry> route = Planner.PlanRoute(NaoState.Instance.Map, locations);
                if (route != null && route.Count != 0)
                    Logger.Log(this, "Path found, walking...");
                else 
                {
                    Logger.Log(this, "No path found.");
                    ReportFailure();
                    return;
                }
                
                Logger.Log(this, "RouteEntries: " + route.Count);
                foreach (RouteEntry entry in route)
                {
                    Logger.Log(entry);
                }
                
                foreach (RouteEntry entry in route)
                {
                    if (!Aborted)
                    {
                        Logger.Log(this, "Turning to " + entry.Direction + "...");
                        Walk.Instance.TurnTo(entry.Direction);
                        MarkerSearchWorker worker = Walk.Instance.WalkTowardsMarker(entry.MarkerID, entry.WantedDistance);
                        worker.Start();
                    }
                }
                
                if (Aborted)
                    ReportFailure();
                else
                    ReportSuccess();
                   
            }
            catch(System.Exception e)
            {
                Logger.Log(this, "Unexpected exception caught: " + e.Message);
                ReportFailure();
            }
        }

        private void CheckSeeObject()
        {
            new LookForObjectEvent().Fire();
        }

        /// <summary>
        /// Reports the successful execution of this event to Goal and posts a location event 
        /// to the event-queue with the Nao's new location.
        /// </summary>
        protected override void ReportSuccess()
        {
            base.ReportSuccess();
            EventQueue.Goal.Post(new LocationEvent(locations[locations.Count-1].X, locations[locations.Count-1].Y));
            CheckSeeObject();
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
