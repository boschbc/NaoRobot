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
        /// Looks for the object and registers its findings.
        /// </summary>
        private void LookForObject()
        {
            Pose.Instance.LookDown();
            Call(() => Walk.Instance.StartWalking(0F, 0F, -0.1F));
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
            bool onceVisible = false;
            bool said = false;
            Pose.Instance.LookDown();
            Walk walk = Walk.Instance;
            while (Running)
            {
                Thread.Sleep(150);
                Rectangle ob = processor.DetectObject();
                if (ob.Width != 0)
                {
                    onceVisible = true;
                    if (Processing.CloseEnough(ob))
                    {
                        PositionedCorrectly = true;
                        Running = false;
                    }
                    else
                    {
                        Call(() => walk.StartWalking(0.4F, 0F, processor.CalculateTheta(ob)));
                    }
                }// no object, but we seen it before
                else if(onceVisible)
                {
                    Logger.Log(this, "Im probably at the object, but i cant see it");
                    PositionedCorrectly = true;
                    // assume connected
                    if (!said)
                    {
                        Logger.Say("I can not see the object.");
                        said = true;
                    }
                }
            }
            walk.StopMoving();
        }
    }
}
