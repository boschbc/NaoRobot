using System;
using System.Collections;
using System.Collections.Generic;

using Aldebaran.Proxies;

using Naovigate.Util;

namespace Naovigate.Grabbing
{
    class CoolGrabber
    {
        private static CoolGrabber instance = null;
        private MotionProxy motion;
        private RobotPostureProxy posture;

        public CoolGrabber()
        {
            motion = NaoState.Instance.MotionProxy;
            posture = NaoState.Instance.PostureProxy;
        }

        public static CoolGrabber Instance
        {
            get
            {
                if (instance == null) instance = new CoolGrabber();
                return instance;
            }
        }

        public void doSomething()
        {
            posture.goToPosture("Stand", 1F);
            float speed = 0.3f;
            ArrayList names = new ArrayList(8);
            names.Add("LArm");
            names.Add("RArm");
            motion.setStiffnesses(names, 1F);

            names.Clear();
            names.Add("LShoulderRoll");
            names.Add("RShoulderRoll");
            names.Add("LElbowYaw");
            names.Add("RElbowYaw");
            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");
            names.Add("LHand");
            names.Add("RHand");
            ArrayList angles =new ArrayList(8);
            angles.Add(1.3265F);
            angles.Add(-1.3265F);
            angles.Add(-1.2F);
            angles.Add(1.2F);
            angles.Add(0.5F);
            angles.Add(0.5F);
            angles.Add(1F);
            angles.Add(1F);            
            motion.angleInterpolationWithSpeed(names, angles, speed);

            names.Clear();
            names.Add("LShoulderRoll");
            names.Add("RShoulderRoll");
            angles.Clear();
            angles.Add(-0.3142F);
            angles.Add(0.3142F);
            motion.angleInterpolationWithSpeed(names, angles, speed);

            names.Clear();
            names.Add("LHand");
            names.Add("RHand");

            motion.setStiffnesses(names, 0.4F);

            angles.Clear();
            angles.Add(0F);
            angles.Add(0F);
            motion.angleInterpolationWithSpeed(names, angles, speed);

            names.Clear();
            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");
            angles.Clear();
            angles.Add(0F);
            angles.Add(0F);
            motion.angleInterpolationWithSpeed(names, angles, speed);

            names.Clear();
            names.Add("LElbowRoll");
            names.Add("RElbowRoll");
            angles.Clear();
            angles.Add(-1.4F);
            angles.Add(1.4F);
            motion.angleInterpolationWithSpeed(names, angles, speed);

            names.Clear();
            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");
            angles.Clear();
            angles.Add(1.4F);
            angles.Add(1.4F);
            motion.angleInterpolationWithSpeed(names, angles, speed);
        }
        
    }
}
