using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.Event.Internal
{
    public sealed class TestEvent : NaoEvent
    {
        public TestEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                MainProgram.Test();
            }
            catch (Exception e)
            {
                Util.Logger.Except(e);
            }
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
