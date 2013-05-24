using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Aldebaran.Proxies;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Grabbing
{
    /// <description>Class that handles the 'grab' command. Grab an element right in front of the robot</description>    class Grabber
    public class Grabber
    {
        public static readonly float grabSpeed = 0.5f;
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

        public MotionProxy Motion
        {
            get { return motion; }
        }

        public RobotPostureProxy Posture
        {
            get { return posture; }
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
            posture.goToPosture("Stand", 1F);
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
            ArrayList angles = new ArrayList(8);
            angles.Add(1.3265F);
            angles.Add(-1.3265F);
            angles.Add(-1.2F);
            angles.Add(1.2F);
            angles.Add(0.5F);
            angles.Add(0.5F);
            angles.Add(1F);
            angles.Add(1F);
            motion.angleInterpolationWithSpeed(names, angles, grabSpeed);

            names.Clear();
            names.Add("LShoulderRoll");
            names.Add("RShoulderRoll");
            angles.Clear();
            angles.Add(-0.3142F);
            angles.Add(0.3142F);
            motion.angleInterpolationWithSpeed(names, angles, grabSpeed);

            names.Clear();
            names.Add("LHand");
            names.Add("RHand");

            motion.setStiffnesses(names, 0.4F);

            angles.Clear();
            angles.Add(0F);
            angles.Add(0F);
            motion.angleInterpolationWithSpeed(names, angles, grabSpeed);

            names.Clear();
            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");
            angles.Clear();
            angles.Add(0F);
            angles.Add(0F);
            motion.angleInterpolationWithSpeed(names, angles, grabSpeed);

            names.Clear();
            names.Add("LElbowRoll");
            names.Add("RElbowRoll");
            angles.Clear();
            angles.Add(-1.4F);
            angles.Add(1.4F);
            motion.angleInterpolationWithSpeed(names, angles, grabSpeed);

            names.Clear();
            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");
            angles.Clear();
            angles.Add(1.4F);
            angles.Add(1.4F);
            motion.angleInterpolationWithSpeed(names, angles, grabSpeed);
        }
        /*
         * put down the object the nao is holding
         */
        public void PutDown()
        {
            float kneelDepth = 1f;
            NaoState.Instance.SpeechProxy.say("Put Down");

            if (Pose.Instance.Balanced)
            {
                NaoState.Instance.SpeechProxy.say("Stable");
                Pose.Instance.Kneel(kneelDepth);
            }
            else{
                NaoState.Instance.SpeechProxy.say("Unstable");
            }
            Release();
            Pose.Instance.StandUp();
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
            
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("RShoulderRoll");
            names.Add("LShoulderRoll");
            names.Add("RHand");
            names.Add("LHand");
            // spread arms
            angles.Add(-0.25f);
            angles.Add(0.25f);
            // release hands
            angles.Add(1);
            angles.Add(1);

            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }

        public static void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
