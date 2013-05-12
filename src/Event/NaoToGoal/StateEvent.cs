using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send the state the Nao is in, state being one of:
     *      walking
     *      looking
     *      stopped
     */
    public class StateEvent : NaoToGoalEvent
    {
        private int state;

        /*
         * Explicit constructor.
         */
        public StateEvent(int state)
        {
            this.state = state;
        }

        /*
         * Implicit constructor.
         */
        public StateEvent()
        {
            //state = get the nao's state
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
