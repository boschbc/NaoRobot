using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Movement;

namespace Naovigate.Event
{
    class NaoCollidingEvent : NaoEvent 
    {
        public new static readonly EventCode code = EventCode.Colliding;
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
            Walk.Instance.StopMove();
        }

        /*
         * unimplemented
         */
        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
