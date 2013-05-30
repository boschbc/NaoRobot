using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao to sit down.
    /// </summary>
    class ShutdownEvent : NaoEvent
    {
        public ShutdownEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Pose.Instance.Crouch();
            Util.NaoState.Instance.MotionProxy.setStiffnesses("Body", 0F);
        }

        /// <summary>
        /// Aborts execution of the event.
        /// </summary>
        public override void Abort()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return "ShutdownEvent";
        }
    }
}
