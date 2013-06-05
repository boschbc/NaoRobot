using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which makes the Nao turn.
    /// </summary>
    class TurnEvent : NaoEvent
    {
        private float rotation;
        private float accuracy;

        public TurnEvent(float rotation, float accuracy)
        {
            this.rotation = rotation;
            this.accuracy = accuracy;
        }

        public TurnEvent(float rotation)
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
        public override void Abort() { }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Turn; }
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return "TurnEvent";
        }
    }
}
