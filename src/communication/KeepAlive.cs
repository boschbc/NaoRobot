using System;
using Naovigate.Util;
using System.Threading;
using Naovigate.Event;
using Naovigate.Event.Internal;

namespace Naovigate.Communication
{
    public class KeepAlive : ActionExecutor
    {
        private ICommunicationStream stream;
        public KeepAlive(ICommunicationStream stream)
        {
            this.stream = stream;
        }

        public override void Run()
        {
            while (Running)
            {
                Thread.Sleep(10000);
                try
                {
                    EventQueue.Goal.Post(new KeepAliveEvent());
                }
                catch
                {
                    stream.Open = false;
                    while (!stream.Open) Thread.Sleep(100);
                }
            }
        }
    }
}
