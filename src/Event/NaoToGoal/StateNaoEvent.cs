using System;
using Naovigate.Communication;

namespace Naovigate.Event.NaoToGoal
{
    /**
     * Send the state the Nao is in, state being:
     *      walking
     *      looking
     *      stopped
     *      
     * Note: which byte represents which state has not been defined yet
     */
    public class StateNaoEvent : DataSendingNaoEvent
    {
        private static readonly byte eventID = 0x8f;

        /**
         * create a new StateNaoEvent, with the specified state to be sent
         */
        public StateNaoEvent(byte state) : base(eventID, state) { }

        /**
         * Fires the event.
         **/
        public override void Fire()
        {
            SendAsByte();
        }
    }
}
