using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * The Nao is Distance rooms away from MarkerID
     */
    public class DistanceToEvent : NaoToGoalEvent
    {
        private float distance;

        /*
         * Implicit constructor.
         */
        public DistanceToEvent(int markerID)
        {
            distance = 0;//distance = get the Nao's distance from a marker.
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            //Send over the network to goal
        }
    }
}
