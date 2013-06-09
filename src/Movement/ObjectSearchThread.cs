using System;
using System.Threading;
using System.Collections;
using Naovigate.Util;
using Naovigate.Movement;
using Naovigate.Vision;
using Naovigate.Grabbing;
using System.Drawing;

namespace Naovigate.Movement
{
    internal sealed class ObjectSearchThread : ActionExecutor
    {
        private int objectId;
        private Camera camera;

        public ObjectSearchThread(int objectID)
        {
            this.objectId = objectID;
            camera = new Camera("ObjectSearch");
            camera.Subscribe();
            camera.CalibrateCamera(3);
        }


        /// <summary>
        /// True if the Nao managed to position itself in front of the object.
        /// </summary>
        public bool PositionedCorrectly
        {
            get;
            private set;
        }

        /// <summary>
        /// True if the object was found.
        /// </summary>
        public bool ObjectFound
        {
            get;
            private set;
        }

        public override void Run()
        {
            Call(LookForObject);
            if (ObjectFound)
                Call(GoInfrontOfObject);
        }

        /// <summary>
        /// Looks for the object and registers its findings.
        /// </summary>
        private void LookForObject()
        {
            Pose.Instance.Look(0.5F);
            Processing processor = new Processing(camera);
            Call(() => Walk.Instance.StartWalking(0F,0F,-0.1F));
            while (Running && !ObjectFound)
            {
                Rectangle ob = processor.DetectObject();
                ObjectFound = (ob.Width != 0);
                if (ObjectFound)
                {
                    Walk.Instance.StopMoving();
                }
                Thread.Sleep(150);
            }
        }

        private void GoInfrontOfObject()
        {
            Pose.Instance.Look(0.5F);
            Processing processor = new Processing(camera);
            Walk walk = Walk.Instance;
            while (Running)
            {
                Thread.Sleep(150);
                Rectangle ob = processor.DetectObject();
                if (ob.Width != 0)
                {
                    if (Processing.closeEnough(ob))
                    {
                        PositionedCorrectly = true;
                        Running = false;
                    }
                    else
                    {
                        Call(() => walk.StartWalking(0.4F, 0F, processor.calculateTheta(ob)));
                    }
                }
            }
            walk.StopMoving();
        }
    }
}
