using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Movement;

namespace Naovigate.Event
{
    public sealed class NaoCollidingEvent : NaoEvent 
    {
        private bool left, right;

        public NaoCollidingEvent(bool left, bool right)
        {
            this.left = left;
            this.right = right;
            Priority = Priority.High;
        }

        public override void Fire()
        {
            Walk.Instance.StopLooking();
            Walk.Instance.StopMoving();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Colliding; }
        }
    }
}
