using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao stand up.
    /// </summary>
    public sealed class StandUpEvent : NaoEvent
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
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.StandUp; }
        }
    }
}
