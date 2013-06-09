using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao to sit down.
    /// </summary>
    public class ShutdownEvent : NaoEvent
    {
        public ShutdownEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Pose.Instance.Crouch();
            Util.Proxies.GetProxy<Aldebaran.Proxies.MotionProxy>().setStiffnesses("Body", 0F);
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public override void Abort(){}

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.ShutDown; }
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
