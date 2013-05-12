using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Nao sees object ID at marker MarkerID
     */
    public class SeeEvent : NaoToGoalEvent
    {
        private int objectID;

        /*
         * Explicit constructor.
         */
        public SeeEvent(int objectID)
        {
            this.objectID = objectID;
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
