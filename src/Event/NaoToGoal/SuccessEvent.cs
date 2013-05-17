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
         * @param id The ID of the event which was successfully completed.
         */
        public SuccessEvent(int eventID) : base((byte)EventCode.Success, eventID) { }

        /*
         * Fires the event.
         */
        public override void Fire()
        {
            SendAsByte();
        }
    }
}
