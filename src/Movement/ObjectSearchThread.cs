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

        public ObjectSearchThread(int objectID)
        {
            //TODO
        }

        public ObjectSearchThread(int objectID, double dist)
        {
            this.objectId = objectID;
            this.dist = dist;
        }

        /// <summary>
        /// True if the object was found.
        /// </summary>
        public bool ObjectFound
        {
            get { return true; }
        }

        public override void Run()
        {
            Call(() => LookForObject());
        }

        //Looks for a object and then dissects the data
        public void LookForObject()
        {
            cm = new Camera("ObjectSearch");
            Processing processor = new Processing(cm);
            while (running)
            {
                ArrayList ObjectData = processor.DetectObject();
                if (ObjectData.Count == 2)
                {
                    Console.WriteLine("Fire pickup event with the data from the ObjectData");
                    //while in the pickup event is running keep getting the objectData
                    //The object data -> objectdata[0] = distance objectData[1] = angle;
                }
            }
            Console.WriteLine("Exit LookForObjects");
        }
    }
}
