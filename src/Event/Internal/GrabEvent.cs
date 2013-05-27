using System;

using Naovigate.Grabbing;

namespace Naovigate.Event.Internal
{
    class GrabEvent : NaoEvent
    {
        public GrabEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Grabber.Instance.Grab();
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
            return "GrabEvent";
        }
    }
}
