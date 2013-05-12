using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Nao is at object ID
     */
    public class AtObjectEvent : NaoToGoalEvent
    {
        private int id;

        /*
         * Explicit constructor.
         */
        public AtObjectEvent(int id)
        {
            this.id = id;
        }

        /*
         * Implicit constructor.
         */
        public AtObjectEvent()
        {
            //id = get the object's id the Nao is at
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
