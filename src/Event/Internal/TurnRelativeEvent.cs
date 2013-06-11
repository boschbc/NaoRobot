﻿
using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao turn.
    /// </summary>
    public sealed class TurnRelativeEvent : NaoEvent
    {
        private float rotation;
        private float accuracy;

        public TurnRelativeEvent(float rotation, float accuracy)
        {
            this.rotation = rotation;
            this.accuracy = accuracy;
        }

        public TurnRelativeEvent(float rotation)
        {
            this.rotation = rotation;
            this.accuracy = 0.01f;
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Walk.Instance.TurnExact(rotation, accuracy);
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