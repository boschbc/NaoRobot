using System;
using System.Threading;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event which does useless stuff, because we can.
    /// </summary>
    public sealed class PerformanceEvent : NaoEvent
    {
        public PerformanceEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            long sent = 0;
            long time = DateTime.Now.Millisecond;
            System.IO.Stream s = Stream.InternalStream;
            while (!Aborted)
            {
                byte[] buf = new byte[1 << 16];
                s.Write(buf, 0, buf.Length);
                sent += buf.Length;
                Stream.WriteNewline();
                long cur = DateTime.Now.Millisecond - time;
                Console.WriteLine("Speed: "+(1000*(sent/1024/1024)/cur)+" MB/S");
                Console.WriteLine(sent);
                Console.WriteLine(cur);
            }
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.StandUp; }
        }
    }
}
