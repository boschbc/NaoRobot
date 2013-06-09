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
        /// Aborts execution of the event.
        /// </summary>
        public override void Abort() { }

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
            return "TestEvent";
        }
    }
}
