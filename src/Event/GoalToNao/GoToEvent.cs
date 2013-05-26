using System;
using System.Collections.Generic;
using System.Drawing;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;

namespace Naovigate.Event.GoalToNao
{
    /*
     * @param MarkerID the id of a marker.
     * @param Distance the Nao will stop Distance rooms away from the marker.
     * Move to the marker MarkerID, if it is in range of the Nao.
     * The Nao will stop Distance rooms away from the marker.
     */
    public class GoToEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.GoTo;

        private List<Point> locations;
        private bool aborted;

        private MarkerSearchThread worker;

        /*
         * Default constructor.
         */
        public GoToEvent()
        {
            Unpack();
        }

        /*
         * Explicit constructor.
         */
        public GoToEvent(List<Point> locations)
        {

        }
        
        /*
         * Explicit constructor.
         */
        public GoToEvent(params Point[] locations) : this(new List<Point>(locations)) { }

        /*
         * Extract the MarkerID and Distance parameters from a communication stream.
         */
        private void Unpack()
        {
            int nodes = stream.ReadInt();
            locations = new List<Point>();
            for (int i = 0; i < nodes;i++ )
            {
                locations.Add(new Point(stream.ReadInt(), stream.ReadInt()));
            }
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code);
            try
            {
                List<Point> markersToGoTo = null;// get from map
                for (int i = 0;!aborted &&  i < markersToGoTo.Count;i++ )
                {
                    worker = Walk.Instance.WalkTowardsMarker(0, markersToGoTo[i].X, markersToGoTo[i].Y);
                }
                
            }
            catch
            {
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
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

        
    }
}
