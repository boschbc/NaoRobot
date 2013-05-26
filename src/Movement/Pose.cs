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
        private static readonly ArrayList kneelNames = new ArrayList(new String[] { "LHipPitch", "RHipPitch", "LKneePitch", "RKneePitch", "LAnklePitch", "RAnklePitch" });
        
        
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
        /// Have the Nao stand up.
        /// </summary>
        public void StandUp()
        {
            posture.goToPosture("Stand", 0.5f);
        }

        /// <summary>
        /// Have the Nao sit-down.
        /// </summary>
        public void SitDown()
        {
            posture.goToPosture("Sit", 0.5f);
        }

        /// <summary>
        /// Have the Nao crouch.
        /// </summary>
        public void Crouch()
        {
            posture.goToPosture("Crouch", 0.5f);
        }

        /// <summary>
        /// Make the Nao kneel, with specified depth.
        /// </summary>
        /// <param name="depth">0 means standing. 1 means sitting.</param>
        public void Kneel(float depth)
        {
            if (depth > 1f) depth = 1f;
            if (depth < 0f) depth = 0;
            ArrayList angles = new ArrayList();

            angles.Add(-depth);//low
            angles.Add(-depth);

            angles.Add(depth + depth);//high
            angles.Add(depth * 2);

            angles.Add(-depth);//low
            angles.Add(-depth);

            motion.angleInterpolationWithSpeed(kneelNames, angles, 0.3F);
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
