using System;
using Naovigate.Communication;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Send information regarding the successful completion of an action-event.
    /// </summary>
    public sealed class FailureEvent : DataSendingNaoEvent
    {
        private byte failedEventCode;

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="eventCode">The code of the event which failed.</param>
        public FailureEvent(EventCode eventCode) : base(EventCode.Failure, (int)eventCode)
        {
            failedEventCode = (byte)eventCode;
        }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="eventCode">The code of the event which failed.</param>
        public FailureEvent(byte eventCode) : base(EventCode.Failure, eventCode) 
        {
            failedEventCode = eventCode;
        }

        /// <summary>
        /// Send this event across the connection.
        /// </summary>
        private new void Send()
        {
            SendAsByte();
            if (Stream.GetType() == typeof(BitStringCommunicationStream))
                Stream.WriteNewline();
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A human readable string.</returns>
        public override string ToString()
        {
            return "FailureEvent<eventCode = " + failedEventCode + ">";
        }
    }
}
