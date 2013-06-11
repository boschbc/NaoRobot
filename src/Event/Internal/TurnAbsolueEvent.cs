
using Naovigate.Movement;
using Naovigate.Navigation;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao turn.
    /// </summary>
    public sealed class TurnAbsoluteEvent : NaoEvent
    {
        private Direction direction;
        private float accuracy;

        public TurnAbsoluteEvent(Direction dir)
        {
            this.direction = dir;
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Walk.Instance.TurnAbsolute(direction);
        }

        /// <summary>
        /// Aborts execution of the event.
        /// </summary>
        public override void Abort()
        {
            base.Abort();
            Walk.Instance.StopMoving();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Turn; }
        }
    }
}
