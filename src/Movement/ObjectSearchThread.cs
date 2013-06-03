using System;
using System.Threading;
using System.Collections;
using Naovigate.Util;
using Naovigate.Movement;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    public class ObjectSearchThread : ActionExecutor
    {
        private bool found;
        private int objectId;
        private double dist;
        private Camera cm;

        public ObjectSearchThread(int objectID, double dist)
        {
            this.objectId = objectID;
            this.dist = dist;
        }

        public override void Run()
        {
            LookForObject();
        }

        //Looks for a object and then dissects the data
        public void LookForObject()
        {
            cm = new Camera("ObjectSearch");
            Processing processor = new Processing(cm);
            while (running)
            {
                processor.DetectObject();
            }
            Console.WriteLine("Exit LookForObjects");
        }
    }
}
