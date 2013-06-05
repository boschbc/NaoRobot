using System;
using System.Threading;
using System.Collections;
using Naovigate.Util;
using Naovigate.Movement;
using Naovigate.Vision;
using Naovigate.Grabbing;

namespace Naovigate.Movement
{
    public class ObjectPickupThread : ActionExecutor
    {
        private int objectId;
        private double dist;
        private Camera camera;

        public ObjectPickupThread(int objectID)
        {
            this.objectId = objectID;
        }

        public ObjectPickupThread(int objectID, double dist)
        {
            this.objectId = objectID;
            this.dist = dist;
        }

        /// <summary>
        /// True if the object was found.
        /// </summary>
        public bool ObjectFound
        {
            get;
            private set;
        }

        public float DistanceToObject
        {
            get;
            private set;
        }

        public float AngleToObject
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
            camera = new Camera("ObjectSearch");
            Processing processor = new Processing(camera);
            while (Running)
            {
                ArrayList ObjectData = processor.DetectObject();
                if (ObjectData.Count == 2)
                {
                    DistanceToObject = (float) ObjectData[0];
                    AngleToObject = (float) ObjectData[1];
                }
            }
        }

        private void GoInfrontOfObject()
        {
            if (!ObjectFound)
                throw new InvalidOperationException(
                    "Must detect an object before positioning in front of it.");

            Call(TurnTowardsObject);
            Call(WalkTowardsObject);
        }

        private void TurnTowardsObject()
        {
            Walk.Instance.TurnExact(AngleToObject, 0.05f);
        }

        private void WalkTowardsObject()
        {
            float threshold = 0.3f;  //Rotate towards object when the angle to it exceeds this.
            float speed = 0.5f;  //How fast to walk.
            float closeEnough = 0.5f;  //The distance (in m) to stop before the object.
            while (Running && DistanceToObject > closeEnough)
            {
                Call(LookForObject);
                Logger.Log(this, "Angle to object: " + AngleToObject);
                Logger.Log(this, "Distance to object: " + DistanceToObject);
                if (AngleToObject > threshold)
                {
                    Call(Walk.Instance.StopMoving);
                    Call(() => Walk.Instance.TurnExact(AngleToObject, 0.3f));
                    Call(() => Walk.Instance.StartWalking(speed, 0f, 0f));
                }
                else if (!Walk.Instance.IsMoving()) 
                    Call(() => Walk.Instance.StartWalking(0.5f, 0f, 0f));
            }
            Walk.Instance.StopMoving();
        }
    }
}
