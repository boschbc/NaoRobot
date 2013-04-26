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
        private static MotionProxy motion;
        private static RobotPostureProxy posture;

        public static void RefreshProxies()
        {
            motion = NaoState.GetMotionProxy();
            posture = NaoState.GetRobotPostureProxy();
        }

        public static void WalkTo(float x, float y, float theta)
        {
            //Util.NaoState.IsWalking = true;
            posture.goToPosture("StandZero", 1f);
            motion.moveInit();
            motion.move(x, y, theta);
            motion.stopMove();
        }

        public static void stopMove()
        {
            motion.stopMove();
           // Util.NaoState.IsWalking = false;
        }
    }
}
