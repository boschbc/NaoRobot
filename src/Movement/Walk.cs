﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

using Aldebaran.Proxies;

using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    public class Walk
    {
        private MotionProxy motion; 
        private RobotPostureProxy posture; 

        private static Walk instance = null;

        public Walk()
        {
            motion = NaoState.Instance.MotionProxy;
            posture = NaoState.Instance.PostureProxy;
            instance = this;
        }

        public static Walk Instance
        {
            get {
                if (instance == null)
                {
                    instance = new Walk();
                }
                return instance;
            }
            set { instance = value; }
        }

        public bool WalkWithObject
        {
            set
            {
                if (value) motion.setWalkArmsEnable(false, false);
                else motion.setWalkArmsEnable(true, true);
            }
        }

        /*
         * walk to (x, y, theta) with the Nao as the origin
         * */
        public void WalkTo(float x, float y, float theta)
        {
            InitMove();
            motion.post.moveTo(x, y, theta);
        }

        public void WalkWhileHolding()
        {
            motion.setWalkArmsEnable(false, false);
            motion.moveToward(0.2F, 0, 0);
            motion.setWalkArmsEnable(true, true);
        }

        /*
         * Sets the stiffness of the Nao's motors on if it is not already so.
         */
        public void InitMove()
        {
            if (!motion.robotIsWakeUp())
                motion.wakeUp();
            if (!motion.moveIsActive())
                motion.moveInit();
        }

        /*
         * Start walking with normalized speed x, y and theta
         * */
        public void StartWalking(float x, float y, float theta)
        {
            InitMove();
            motion.moveToward(x, y, theta);
        }

        /*
         * Turn (normalized) dir and walk till the Nao is within dist pieces of wall of the marker with MarkID = markerID
         */
        public MarkerSearchThread WalkTowardsMarker(float dir, int markerID, double dist)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            MarkerSearchThread t = new MarkerSearchThread(markerID,dist);
            t.Start();
            return t;
        }

        /*
         * Turn (normalized) dir and walk till the Nao is within dist pieces of wall of the object with id ObjectID
         */
        public virtual ObjectSearchThread WalkTowardsObject(float dir, int objectID, double dist)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            ObjectSearchThread t = new ObjectSearchThread(objectID, dist);
            t.Start();
            return t;
        }

        /*
         * stop looking for the marker and stop moving
         * */
        public void StopLooking()
        {
            StopMove();
        }

        /* 
         * return true iff the Nao is moving
         * */
        public Boolean IsMoving()
        {
            try
            {
                return motion.moveIsActive();
            }
            catch
            {
                Console.WriteLine("MotionError - Moving");
                // proxy is busy (moving) so return true
                return true;
            }
        }

        /*
         * stop the Nao from moving
         * */
        public void StopMove()
        {
            motion.stopMove();
        }

        public void Abort()
        {
            StopMove();
        }
    }
}
