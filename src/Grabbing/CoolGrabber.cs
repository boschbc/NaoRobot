using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Aldebaran.Proxies;

namespace Naovigate.Grabbing
{
    class CoolGrabber
    {
        private static CoolGrabber instance = null;
        private MotionProxy motion;
        private RobotPostureProxy posture;

        public CoolGrabber()
        {
            motion = Naovigate.Util.NaoState.MotionProxy;
            posture = Naovigate.Util.NaoState.PostureProxy;
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

            ArrayList names = new ArrayList(2);
            names.Add("LArm");
            names.Add("RArm");
            motion.setStiffnesses(names, 1F);
            
            names = new ArrayList(8);
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
            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
            
            names = new ArrayList(2);
            names.Add("LShoulderRoll");
            names.Add("RShoulderRoll");
            angles = new ArrayList(2);
            angles.Add(-0.3142F);
            angles.Add(0.3142F);
            motion.angleInterpolationWithSpeed(names, angles, 0.3F);

            names = new ArrayList(2);
            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");
            angles = new ArrayList(2);
            angles.Add(0.3F);
            angles.Add(0.3F);
            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }
        
    }
}
