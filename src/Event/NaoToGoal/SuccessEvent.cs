using System;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send information regarding the successful completion of an action-event.
     */
    public class SuccessEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Success;

        /*
         * Explicit constructor.
         * @param id The ID of the event which was successfully completed.
         */
        public SuccessEvent(EventCode eventCode) : base((byte)EventCode.Success, (int)eventCode) { }

        /*
         * Fires the event.
         */
        public override void Fire()
        {
            SendAsByte();
        }
    }
}
