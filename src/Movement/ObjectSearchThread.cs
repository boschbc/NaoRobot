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
        private int markerID;
        private double dist;

        public ObjectSearchThread(int markerID, double dist)
        {
            this.markerID = markerID;
            this.dist = dist;
        }

        public override void Run()
        {
            LookForObject();
        }

        public void LookForObject()
        {
           
        }
    }
}
