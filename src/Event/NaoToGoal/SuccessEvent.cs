﻿using System;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Send information regarding the successful completion of an action-event.
    /// </summary>
    public class SuccessEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Success;

        private int successfulEventCode;

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="eventCode">Event code of the event which was successfully executed.</param>
        public SuccessEvent(EventCode eventCode) : base((byte)EventCode.Success, (int)eventCode) { }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="eventCode">The code of the event which failed.</param>
        public SuccessEvent(byte eventCode) : base(EventCode.Success, eventCode) 
        {
            successfulEventCode = eventCode;
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            SendAsByte();
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
