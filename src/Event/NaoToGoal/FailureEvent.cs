using System;

namespace Naovigate.Event.NaoToGoal
{
   /*
    * Send information regarding the successful completion of an action-event.
    */
    public class FailureEvent : DataSendingNaoEvent
    {
        /*
         * Explicit constructor.
         */
        public FailureEvent(EventCode code) : base((byte)EventCode.Failure, (int)code) { }

        /*
         * Fires the event.
         */
        public override void Fire()
        {
            SendAsByte();
        }
    }
}
