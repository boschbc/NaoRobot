using System;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Send information regarding an unresolved error.
    /// </summary>
    public class ErrorEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Error;

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        public ErrorEvent() : base((byte)EventCode.Error) { }
    }
}
