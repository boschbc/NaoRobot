using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Aldebaran.Proxies;

using Naovigate.Util;

namespace Naovigate.Movement
{
    class Walk
    {
        private MotionProxy motion;
        private RobotPostureProxy posture;

        private static Walk instance = null;

        public Walk()
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
            motion.moveInit();
            motion.move(x, y, theta);
            motion.stopMove();
        }

        public void StartWalking(float x, float y, float theta)
        {
            if (!IsMoving()) motion.moveInit();
            motion.moveToward(x, y, theta);
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
