﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Vision
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
            get { return instance == null ? new Eyes() : instance; }
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
        /// The Nao will look down.
        /// </summary>
        public void LookDown()
        {
            Logger.Log(this, "Looking down.");
            Pose.Instance.Look(0.5f);
        }

        /// <summary>
        /// The Nao will look straight in front.
        /// </summary>
        public void LookStraight()
        {
            Logger.Log(this, "Looking straight.");
            Pose.Instance.Look(0f);
            Pose.Instance.StartTurningHead(0f);
            Walk.Instance.WaitForMoveToEnd();
        }

        /// <summary>
        /// The Nao will look to its left.
        /// </summary>
        /// <param name="angle">The angle to turn towards (in radians).</param>
        public void LookLeft(float angle)
        {
            Logger.Log(this, "Looking left.");
            Pose.Instance.StartTurningHead(angle);
            Walk.Instance.WaitForMoveToEnd();
        }

        /// <summary>
        /// The Nao will look to its right.
        /// </summary>
        /// <param name="angle">The angle to turn towards (in radians).</param>
        public void LookRight(float angle)
        {
            Logger.Log(this, "Looking right.");
            Pose.Instance.StartTurningHead(-angle);
            Walk.Instance.WaitForMoveToEnd();
        }

        /// <summary>
        /// Looks for objects both left and right of the Nao by turning its head.
        /// Stores the results (if any) at the ObjectDetected property and the angle to 
        /// that object at AngleToObject.
        /// </summary>
        public void LookForObjects()
        {
            ObjectDetected = false;
            AngleToObject = 0f;
            foreach (Action<float> lookMethod in new List<Action<float>>() { LookLeft, LookRight })
            {
                TurnAndLook(lookMethod);
                if (ObjectDetected)
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
        protected void TurnAndLook(Action<float> lookMethod)
        {
            float angle = 0;
            float step = (float)(Math.PI / 5);
            float limit = 2.0857f;
            LookStraight();
            while (angle < limit)
            {
                lookMethod(angle);
                if (Processing.Instance.ObjectInSight())
                {
                    ObjectDetected = true;
                    AngleToObject = Pose.Instance.GetHeadAngle();
                    LookStraight();
                    return;
                }
                angle += step;
            }
        }
    }
}
