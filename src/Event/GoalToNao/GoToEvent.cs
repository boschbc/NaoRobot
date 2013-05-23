using System;

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
        private int theta;
        private int markerID;
        private int distance;

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
        public GoToEvent(int theta, int markerID, int distance)
        {
            this.theta = theta;
            this.markerID = markerID;
            this.distance = distance;
        }

        /*
         * Extract the MarkerID and Distance parameters from a communication stream.
         */
        private void Unpack()
        {
            theta = stream.ReadInt();
            markerID = stream.ReadInt();
            distance = stream.ReadInt();
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code);
            try
            {
                worker = Walk.Instance.WalkTowardsMarker(theta, markerID, distance);
            }
            catch
            {
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Nao.Enqueue(statusEvent);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Abort()
        {
            try
            {
                if (worker != null)
                    worker.Abort();
            }
            catch
            {
                EventQueue.Nao.Enqueue(new ErrorEvent());
            }
        }

        
    }
}
