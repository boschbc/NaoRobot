using System;

namespace Naovigate.Event.NaoToGoal
{
   /*
    * Send information regarding the successful completion of an action-event.
    */
    public class FailureEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Failure;

        /*
         * Explicit constructor.
         * @param id The code of the event which failed.
         */
        public FailureEvent(EventCode eventCode) : base((byte)EventCode.Failure, (int)eventCode) { }

        /*
         * Fires the event.
         */
        public override void Fire()
        {
            SendAsByte();
        }
    }
}
