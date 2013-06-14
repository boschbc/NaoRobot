using System.Drawing;
using System.Threading;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    internal sealed class ObjectSearchWorker : ActionExecutor
    {
        private Camera camera;
        private Processing processor;

        public ObjectSearchWorker()
        {
            camera = new Camera("ObjectSearch");
            camera.Subscribe();
            camera.CalibrateCamera(3);
            processor = new Processing(camera);
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
            Logger.Log(this, "Found object: " + ObjectFound);
            if (ObjectFound)
                Call(GoInfrontOfObject);
        }

        /// <summary>
        /// Looks towards the left and forwards again and returns true iff it sees an object
        /// </summary>
        /// <returns>True iff an object has been seen while turning the head</returns>
        public bool IsObjectLeft()
        {
            Processing processor = new Processing(camera);
            bool seenObject = false;
            Pose pose = Pose.Instance;
            pose.StartTurningHead(2.0857F);
            while (Running && !seenObject && 2.0857F - pose.GetHeadAngle() > 0.05)
            {
                Rectangle ob = processor.DetectObject();
                seenObject = (ob.Width != 0);
            }
            pose.StartTurningHead(0);
            while (pose.GetHeadAngle() > 0.05F)
            {
                Thread.Sleep(150);
            }
            return seenObject;
        }

        /// <summary>
        /// Looks for the object and registers its findings.
        /// </summary>
        private void LookForObject()
        {
            Eyes.Instance.LookDown();
            float theta = 0f;
            if (!processor.ObjectInSight())
                theta = IsObjectLeft() ? 0.2F : -0.2F;
            Call(() => Walk.Instance.StartWalking(0F, 0F, theta));
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
            Logger.Log(this, Running + " " + ObjectFound);
        }

        private void GoInfrontOfObject()
        {
            bool onceVisible = false;
            Eyes.Instance.LookDown();
            Walk walk = Walk.Instance;
            while (!PositionedCorrectly)
            {
                Thread.Sleep(150);
                Rectangle ob = processor.DetectObject();
                if (ob.Width != 0)
                {
                    if (Processing.CloseEnough(ob))
                    {
                        PositionedCorrectly = true;
                        break;
                    }
                    else
                    {
                        Call(() => walk.StartWalking(0.4F, 0F, processor.CalculateTheta(ob)));
                    }
                }// no object, but we seen it before
                else if(onceVisible)
                {
                    Logger.Log(this, "Im probably at the object, but i cant see it");
                }
            }
            walk.StopMoving();
        }
    }
}
