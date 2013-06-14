using System;
using Naovigate.Util;
using System.Threading;

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
                    stream.WriteByte(255);
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
