using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao to sit down.
    /// </summary>
    public sealed class ShutdownEvent : NaoEvent
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
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.ShutDown; }
        }
    }
}
