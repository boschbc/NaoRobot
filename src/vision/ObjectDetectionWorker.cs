using System;
using System.Threading;
using Naovigate.Event;

namespace Naovigate.Vision
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectDetectionWorker
    {
        private EventQueue naoQueue;
        public ObjectDetectionWorker()
        {
            
        }

        public void Start()
        {
            Thread t = new Thread(() => Detect());
            t.Name = "ObjDetect";
            t.Start();
        }

        public void Detect()
        {
            
        }
    }
}
