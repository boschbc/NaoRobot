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
         * @param id The ID of the event which failed.
         */
        public FailureEvent(int eventID) : base((byte)EventCode.Failure, eventID) { }

        /*
         * Fires the event.
         */
        public override void Fire()
        {
            SendAsByte();
        }
    }
}
