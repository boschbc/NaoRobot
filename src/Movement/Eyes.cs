using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Util;
using Naovigate.Vision;
using System.Threading;

namespace Naovigate.Movement
{
    /// <summary>
    /// A class that helps coordinate all head-movement and object / marker detection.
    /// </summary>
    internal class Eyes
    {
        private static Eyes instance;

        /// <summary>
        /// This singleton's instance.
        /// </summary>
        public static Eyes Instance
        {
            get { return instance == null ? instance = new Eyes() : instance; }
            set { instance = value; }
        }

        /// <summary>
        /// True if an object was detected.
        /// This property is freely set by the methods of this class.
        /// After each time you call a method, you may check this property for any results
        /// (if it is relevant to the method you invoked).
        /// </summary>
        public bool ObjectDetected
        {
            get;
            private set;
        }

        /// <summary>
        /// A floating point angle (in radians) to the detected object.
        /// This property is freely set by the methods of this class.
        /// After each time you call a method, you may check this property for any results
        /// (if it is relevant to the method you invoked).
        /// </summary>
        public float AngleToObject
        {
            get;
            private set;
        }

        /// <summary>
        /// True if a marker was detected.
        /// This property is freely set by the methods of this class.
        /// After each time you call a method, you may check this property for any results
        /// (if it is relevant to the method you invoked).
        /// </summary>
        public bool MarkerDetected
        {
            get;
            private set;
        }

        /// <summary>
        /// A floating point angle (in radians) to the detected marker.
        /// This property is freely set by the methods of this class.
        /// After each time you call a method, you may check this property for any results
        /// (if it is relevant to the method you invoked).
        /// </summary>
        public float AngleToMarker
        {
            get;
            private set;
        }

        /// <summary>
        /// Checks whether a given marker is in sight.
        /// Sets the MarkerDetected and AngleToMarker properties according to the results.
        /// </summary>
        /// <param name="markerID">The marker ID.</param>
        /// <returns>True if the given marker ID is currently in sight.</returns>
        public bool MarkerInSight(int markerID)
        {
            return MarkerRecogniser.Instance.GetDetectedMarkers().Contains(markerID);
        }

        /// <summary>
        /// The Nao will look down.
        /// </summary>
        public void LookDown()
        {
            Logger.Log(this, "Looking down.");
            Walk.Instance.InitMove();
            Pose.Instance.Look(0.5f);
        }

        /// <summary>
        /// The Nao will look straight in front.
        /// </summary>
        public void LookStraight()
        {
            Logger.Log(this, "Looking straight.");
            Walk.Instance.InitMove();
            Pose.Instance.StartTurningHead(0f);
            Walk.Instance.WaitForMoveToEnd();
            Pose.Instance.Look(0f);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// The Nao will look to its left.
        /// </summary>
        /// <param name="angle">The angle to turn towards (in radians).</param>
        public void LookLeft(float angle)
        {
            Logger.Log(this, "Looking left.");
            Walk.Instance.InitMove();
            Pose.Instance.StartTurningHead(angle);
            Thread.Sleep(500);
            //Walk.Instance.WaitForMoveToEnd();
        }

        /// <summary>
        /// The Nao will look to its right.
        /// </summary>
        /// <param name="angle">The angle to turn towards (in radians).</param>
        public void LookRight(float angle)
        {
            Logger.Log(this, "Looking right.");
            Walk.Instance.InitMove();
            Pose.Instance.StartTurningHead(-angle);
            Thread.Sleep(500);
            //Walk.Instance.WaitForMoveToEnd();
        }

        /// <summary>
        /// Looks for objects both left and right of the Nao by turning its head.
        /// Stores the results (if any) at the ObjectDetected property and the angle to 
        /// that object at AngleToObject.
        /// </summary>
        public void LookForObjects()
        {
            ObjectDetected = Processing.Instance.ObjectInSight();
            
            /*AngleToObject = 0f;
            
            foreach (Action<float> lookMethod in new List<Action<float>>() { LookLeft, LookRight })
            {
                TurnAndLookForObject(lookMethod);
                LookStraight();
                LookStraight();
                if (ObjectDetected)
                    return;
            }*/
        }

        /// <summary>
        /// Looks for markers both left and right of the Nao by turning its head.
        /// Stores the results (if any) at the MarkerDetected property and the angle to 
        /// that object at AngleToMarker.
        /// </summary>
        public void LookForMarker(int markerID)
        {
            MarkerDetected = false;
            AngleToMarker = 0f;
            foreach (Action<float> lookMethod in new List<Action<float>>() { LookLeft, LookRight })
            {
                Logger.Log(this, "Looking at one direction.");
                TurnAndLookForMarker(markerID, lookMethod);
                LookStraight();
                if (MarkerDetected)
                    return;
            }
        }

        /// <summary>
        /// Turns the Nao's head gradually (in steps) and tries to detect
        /// objects after each step. Stops when an object was detected or when 
        /// max head-yaw is reached.
        /// </summary>
        /// <param name="lookMethod">
        /// The method to use to turn the Nao's head.
        /// (Usually LookRight / LookLeft)
        /// </param>
        protected void TurnAndLook(Action<float> lookMethod, Func<bool> stopLooking)
        {
            float angle = 0;
            float step = 0.2f;
            float limit = 2.0857f;
            LookStraight();
            while (angle < limit)
            {
                Logger.Log(this, angle);
                lookMethod(angle);
                if (stopLooking())
                {
                    LookStraight();
                    return;
                }
                angle += step;
            }
        }

        protected void TurnAndLookForObject(Action<float> lookMethod)
        {
            TurnAndLook(lookMethod,
                new Func<bool>(
                    () => 
                    {
                        if (Processing.Instance.ObjectInSight())
                        {
                            ObjectDetected = true;
                            AngleToObject = Pose.Instance.GetHeadAngle();
                            return true;
                        }
                        else
                            return false;
                    }));
        }

        protected void TurnAndLookForMarker(int markerID, Action<float> lookMethod)
        {
            TurnAndLook(lookMethod,
                new Func<bool>(
                    () =>
                    {
                        if (MarkerInSight(markerID))
                        {
                            MarkerDetected = true;
                            AngleToMarker = Pose.Instance.GetHeadAngle();
                            return true;
                        }
                        else
                            return false;
                    }));
        }
    }
}
