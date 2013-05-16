using System;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send information regarding the successful completion of an action-event.
     */
    public class SuccessEvent : DataSendingNaoEvent
    {
        /*
         * Explicit constructor.
         */
        public SuccessEvent(EventCode code) : base((byte)EventCode.Success, (int)code) { }

        /*
         * Fires the event.
         */
        public override void Fire()
        {
            SendAsByte();
        }
    }
}
