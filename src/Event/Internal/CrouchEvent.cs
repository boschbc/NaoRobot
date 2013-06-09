using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao to sit down.
    /// </summary>
    public sealed class CrouchEvent : NaoEvent
    {
        public CrouchEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Pose.Instance.Crouch();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Crouch; }
        }
    }
}
