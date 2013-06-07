﻿using System;

using Naovigate.Grabbing;
using Naovigate.Util;
namespace Naovigate.Event.Internal
{
    public class GrabEvent : NaoEvent
    {
        public GrabEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Grabber.Instance.Grab();
            Grabber.Instance.WaitFor();
            Logger.Log(this, NaoState.Instance.HoldingObject);
        }

        /// <summary>
        /// Aborts execution of the event.
        /// </summary>
        public override void Abort()
        {
            Naovigate.Util.Logger.Log(this, "Aborted");
            Grabber.Instance.Abort();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Grab; }
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
