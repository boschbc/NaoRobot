﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Movement;

namespace Naovigate.Event
{
    class NaoCollidingEvent : INaoEvent 
    {
        private bool left, right;

        public NaoCollidingEvent(bool left, bool right)
        {
            this.left = left;
            this.right = right;
            Priority = Priority.High;
        }


        public Priority Priority
        {
            get;
            set;
        }

        public void Fire()
        {
            Walk.Instance.StopLooking();
            Walk.Instance.StopMove();
        }
    }
}
