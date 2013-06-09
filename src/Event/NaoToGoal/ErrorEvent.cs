using System;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Send information regarding an unresolved error.
    /// </summary>
    public sealed class ErrorEvent : DataSendingNaoEvent
    {
        /// <summary>
        /// Explicit constructor.
        /// </summary>
        public ErrorEvent() : base((byte)EventCode.Error) { }
    }
}
