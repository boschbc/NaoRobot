using System;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send information regarding an unresolved error.
     */
    public class ErrorEvent : DataSendingNaoEvent
    {
        /*
         * Explicit constructor.
         */
        public ErrorEvent(int id) : base((byte)EventCode.Error, id) { }
    }
}
