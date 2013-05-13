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
    public class StateEvent : DataSendingNaoEvent
    {
        /*
         * Explicit constructor.
         */
        public StateEvent(int state) : base((byte) EventCode.State, state) { }
    }
}
