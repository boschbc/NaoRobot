using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

using Aldebaran.Proxies;

using Naovigate.Util;

namespace Naovigate.Movement
{
    class Walk
    {
        private MotionProxy motion;
        private RobotPostureProxy posture;

        private static Walk instance = null;

        private int markerID = -1;
        private Thread t = null;

        public Walk()
        {
            motion = NaoState.GetMotionProxy();
            posture = NaoState.GetRobotPostureProxy();
        }

        public void RefreshProxies()
        {
            motion = NaoState.GetMotionProxy();
            posture = NaoState.GetRobotPostureProxy();
        }

        public static Walk GetInstance()
        {
            return instance == null ? new Walk() : instance;
        }

        public void WalkTo(float x, float y, float theta)
        {
            if (!IsMoving()) 
                motion.moveInit();
            motion.move(x, y, theta);
            //motion.stopMove();
        }

        public void StartWalking(float x, float y, float theta)
        {
            if (!IsMoving()) 
                motion.moveInit();
            motion.moveToward(x, y, theta);
        }

        public void walkTowards(float dir, int markerID)
        {
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            this.markerID = markerID;

            t = new Thread(new ThreadStart(WalkTillMarker));
        }

        public void WalkTillMarker()
        {
            
        }

        public void StopLooking()
        {
            if (t != null)
            {
                t.Abort();
                t = null;
            }
        }

        public Boolean IsMoving()
        {
            return motion.moveIsActive();
        }

        public void StopMove()
        {
            motion.stopMove();
        }
    }
}
