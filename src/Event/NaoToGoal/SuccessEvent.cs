using Naovigate.Communication;
namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Send information regarding the successful completion of an action-event.
    /// </summary>
    public sealed class SuccessEvent : DataSendingNaoEvent
    {
        private byte successfulEventCode;

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="eventCode">Event code of the event which was successfully executed.</param>
        public SuccessEvent(EventCode eventCode) : this((byte)eventCode) {}

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="eventCode">The code of the event which failed.</param>
        public SuccessEvent(byte eventCode) : base(EventCode.Success, eventCode) 
        {
            successfulEventCode = eventCode;
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A human readable string.</returns>
        public override string ToString()
        {
            return "SuccessEvent<eventCode = " + successfulEventCode + ">";
        }
    }
}
