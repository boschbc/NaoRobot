using System;

using Naovigate.Grabbing;
using Naovigate.Util;
namespace Naovigate.Event.Internal
{
    public sealed class GrabEvent : NaoEvent
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
            base.Abort();
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
    }
}
