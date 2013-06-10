
using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao to sit down.
    /// </summary>
    public sealed class SitDownEvent : NaoEvent
    {
        public SitDownEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Pose.Instance.SitDown();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.SitDown; }
        }
    }
}
