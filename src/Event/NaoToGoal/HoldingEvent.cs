using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Notify Goal that the Nao is holding object ID.
     */
    public class HoldingEvent : NaoToGoalEvent
    {
        private int objectID;

        /*
         * Explicit constructor.
         */
        public HoldingEvent(int objectID)
        {
            this.objectID = objectID;
        }

        /*
         * Implicit constructor.
         */
        public HoldingEvent()
        {
            //objectID = get the object's id the Nao is holding
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
