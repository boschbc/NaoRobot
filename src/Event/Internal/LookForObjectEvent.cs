using Naovigate.Event.NaoToGoal;
using Naovigate.Vision;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao turn.
    /// </summary>
    public sealed class LookForObjectEvent : NaoEvent
    {
        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Eyes.Instance.LookForObjects();
            if (Eyes.Instance.ObjectDetected)
                EventQueue.Goal.Post(new SeeEvent(0, 0));
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.LookForObject; }
        }
    }
}
