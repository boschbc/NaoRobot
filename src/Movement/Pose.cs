using System;
using System.Linq;
using System.Collections;
using System.Threading;
using Aldebaran.Proxies;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Movement
{
    public class Pose
    {
        private static Pose instance;
        MotionProxy motion;
        RobotPostureProxy posture;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Pose()
        {
            motion = NaoState.Instance.MotionProxy;
            posture = NaoState.Instance.PostureProxy;
            instance = this;
        }

        public static Pose Instance
        {
            get
            {
                return instance == null ? instance = new Pose() : instance;
            }
        }

        private static void Add(ArrayList list, String part)
        {
            list.Add("L" + part);
            list.Add("R" + part);
        }

        /// <summary>
        /// Got to a standing posture.
        /// </summary>
        public void StandUp()
        {
            posture.goToPosture("Stand", 0.5f);
        }

        /// <summary>
        /// Make the Nao kneel, with specified depth.
        /// </summary>
        /// <param name="depth">0 means standing. 1 means sitting.</param>
        public void Kneel(float depth)
        {
            if (depth > 1f) depth = 1f;
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            Add(names, "HipPitch");
            Add(names, "KneePitch");
            Add(names, "AnklePitch");

            angles.Add(-depth);//low
            angles.Add(-depth);

            angles.Add(depth * 2);//high
            angles.Add(depth * 2);

            angles.Add(-depth);//low
            angles.Add(-depth);

            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }

        public void Welcome()
        {
            Walk.Instance.InitMove();
            posture.goToPosture("Stand", 1F);

            Kneel(0.5f);
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("HeadPitch");
            angles.Add(1f);

            names.Add("RShoulderPitch");
            names.Add("RElbowRoll");
            names.Add("RElbowYaw");

            angles.Add(0.7f);
            angles.Add(2f);
            angles.Add(0);

            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
            Thread.Sleep(500);
            NaoState.Instance.SpeechProxy.say("Welcome");
            Thread.Sleep(1000);
            posture.goToPosture("Stand", 1F);
        }
    }
}
