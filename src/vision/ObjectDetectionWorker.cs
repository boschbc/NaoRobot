using System;
using System.Threading;
using Naovigate.Event;
using Naovigate.Util;

namespace Naovigate.Vision
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectDetectionWorker
    {
        private EventQueue naoQueue;
        private static readonly int waitTime = 5000;
        public ObjectDetectionWorker()
        {
            naoQueue = EventQueue.Nao;
        }

        public bool Running
        {
            get;
            private set;
        }

        public void Start()
        {
            Thread t = new Thread(() => Detect());
            t.Name = "ObjDetect";
            Running = true;
            t.Start();
        }

        private void Detect()
        {
            Logger.Log(this, "Detecting");
            while (Running)
            {
                Thread.Sleep(Math.Max(waitTime - 1000, 1));
                while(naoQueue.TimeSinceLastEvent < waitTime) Thread.Sleep(100);
                Look();
                // sleep after a look
                Thread.Sleep(waitTime);
            }
            Running = false;
            Logger.Log(this, "Ended");
        }

        private void Look()
        {
            Logger.Log(this, "Look");
            Eyes.Instance.LookForObjects();
            if (Eyes.Instance.ObjectDetected)
            {
                Logger.Log(this, "ObjectFound");
                int todo;
                //TODO send object detected to goal
            }
        }

        public void Abort()
        {
            Running = false;
        }
    }
}
