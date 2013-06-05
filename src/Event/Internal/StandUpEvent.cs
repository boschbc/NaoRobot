using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao stand up.
    /// </summary>
    class StandUpEvent : NaoEvent
    {
        public StandUpEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Pose.Instance.StandUp();
        }

        /// <summary>
        /// Aborts execution of the event.
        /// </summary>
        public override void Abort() {}

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.StandUp; }
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return "StandUpEvent";
        }
    }
}
