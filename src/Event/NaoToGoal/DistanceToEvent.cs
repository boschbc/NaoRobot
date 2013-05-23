using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * The Nao is Distance rooms away from MarkerID
     */
    public class DistanceToEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.DistanceTo;

        /*
         * Implicit constructor.
         */
        public DistanceToEvent(int markerID) : base((byte) EventCode.DistanceTo, markerID, GetDistance(markerID)) { }

        /*
         * Explicit constructor.
         */
        public DistanceToEvent(int markerID, int distance) : base((byte) EventCode.DistanceTo, markerID, distance) { }

        /*
         * Calculates distance to given marker.
         */
        private static int GetDistance(int markerID)
        {
            //calculate distance
            return 0;
        }
    }
}
