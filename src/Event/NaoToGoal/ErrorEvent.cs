using System;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send information regarding an unresolved error.
     */
    public class ErrorEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Error;

        /*
         * Explicit constructor.
         */
        public ErrorEvent() : base((byte)EventCode.Error) { }
    }
}
