using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Aldebaran.Proxies;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Movement
{
    public class Pose
    {
        private static readonly bool ignoreStabalise = true;
        private static readonly float maxAllowedDifference = 0.001f;
        private static readonly float attemptStabaliseLimit = 0.3f;

        private static readonly ArrayList rLegNames = new ArrayList(new string[]{"RHipYawPitch", "RHipRoll", "RHipPitch", "RKneePitch", "RAnklePitch", "RAnkleRoll"});
        private static readonly ArrayList lLegNames = new ArrayList(new string[] { "LHipYawPitch", "LHipRoll", "LHipPitch", "LKneePitch", "LAnklePitch", "LAnkleRoll" });

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

        public bool Balanced
        {
            get
            {
                Console.WriteLine();
                if (!IsStable()) AttemptStabalise();
                return IsStable();
            }
        }

        public bool IsStable()
        {
            List<float> rAngles = motion.getAngles(rLegNames, false);
            List<float> lAngles = motion.getAngles(lLegNames, false);
            
            bool stable = true;
            for (int i = 0; stable && i < 6; i++)
            {
                Console.WriteLine(lAngles[i] + " - " + rAngles[i] + " Diff = " + (lAngles[i] - rAngles[i]));
                stable = stable && Math.Abs(lAngles[i] - rAngles[i]) < maxAllowedDifference;
            }
            Console.WriteLine("Stable: " + stable);
            return stable;
        }

        /*
         * Attempt to stabalise the robot if the angles dont differ to much.
         */
        private bool AttemptStabalise()
        {
            if (ignoreStabalise) return false;
            List<float> left = motion.getAngles(lLegNames, false);
            List<float> right = motion.getAngles(rLegNames, false);
            ArrayList angles = new ArrayList();
            for (int i = 0; i < 6;i++ )
            {
                if (Math.Abs(left[i] - right[i]) < attemptStabaliseLimit)
                {
                    angles.Add((left[i]+right[i])/2);
                }
            }
            if (angles.Count == 6)
            {
                ArrayList names = new ArrayList(lLegNames);
                names.AddRange(rLegNames);
                angles.AddRange(angles);
                motion.setAngles(names, angles, 0.1f);
            }
            return angles.Count == 6;
        }
    }
}
