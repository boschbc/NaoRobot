using System;
using System.Linq;
using System.Collections;
using Aldebaran.Proxies;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Grabbing
{
    /// <description>Class that handles the 'grab' command. Grab an element right in front of the robot</description>    class Grabber
    public class Grabber
    {
        private static Grabber instance;
        MotionProxy motion;
        RobotPostureProxy posture;
        
        /*
         * Constructor
         */
        public Grabber()
        {
            motion = NaoState.Instance.MotionProxy;
            posture = NaoState.Instance.PostureProxy;
            instance = this;
        }
        
        public static Grabber Instance
        {
            get
            {
                return instance == null ? instance = new Grabber() : instance;
            }
            set { instance = value; }
        }

        /*
         * The movemont for the grabbing
         */
        public void Grab()
        {

        }
        /*
         * put down the object the nao is holding
         */
        public void PutDown()
        {
            Walk.Instance.InitMove();
            NaoState.Instance.SpeechProxy.say("Put Down Object");
            ArrayList names = new ArrayList(2);
            names.Add("LArm");
            names.Add("RArm");
            motion.setStiffnesses(names, 1F);

            float armsDown = 0.5f;
            float kneelDepth = 1f;
            Pose.Instance.Kneel(kneelDepth);
            LowerArms(armsDown);
            Release();
            posture.goToPosture("Stand", 1F);
        }

        /*
         * lower the arms
         */
        private void LowerArms(float armsDown)
        {
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");

            angles.Add(armsDown);
            angles.Add(armsDown);

            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }

        /*
         * release the object
         */
        private void Release()
        {
            // spread arms
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("RShoulderRoll");
            names.Add("LShoulderRoll");
            angles.Add(-0.25f);
            angles.Add(0.25f);

            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }

        public static void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
