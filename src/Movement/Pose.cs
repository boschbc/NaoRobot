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
        private static long lastStabiliseAttempt = DateTime.Now.Ticks;
        private static readonly bool ignoreStabalise = false;
        private static readonly float maxAllowedDifference = 0.3f;
        private static readonly float attemptStabaliseLimit = 0.3f;

        private static readonly ArrayList rLegNames = new ArrayList(new string[]{"RHipYawPitch", "RHipRoll", "RHipPitch", "RKneePitch", "RAnklePitch", "RAnkleRoll"});
        private static readonly ArrayList lLegNames = new ArrayList(new string[] { "LHipYawPitch", "LHipRoll", "LHipPitch", "LKneePitch", "LAnklePitch", "LAnkleRoll" });
        private static readonly ArrayList kneelNames = new ArrayList(new String[] { "LHipPitch", "RHipPitch", "LKneePitch", "RKneePitch", "LAnklePitch", "RAnklePitch" });

        
        private static Pose instance;

        /// <summary>
        /// This singleton's instance.
        /// </summary>
        public static Pose Instance
        {
            get
            {
                return instance == null ? instance = new Pose() : instance;
            }
        }

        private float lastDepth;

        private MotionProxy motion;
        private RobotPostureProxy posture;

        /// <summary>
        /// A motion proxy.
        /// </summary>
        private MotionProxy Motion
        {
            get
            {
                if (NaoState.Instance.Connected)
                {
                    if (motion == null)
                        motion = Proxies.GetProxy<MotionProxy>();
                }
                else
                    motion = null;
                return motion;
            }
            set { motion = value; }
        }

        /// <summary>
        /// A robot posture proxy.
        /// </summary>
        private RobotPostureProxy Posture
        {
            get
            {
                if (NaoState.Instance.Connected)
                {
                    if (posture == null)
                        posture = Proxies.GetProxy<RobotPostureProxy>();
                }
                else
                    posture = null;
                return posture;
            }
            set { posture = value; }
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Pose()
        {
            NaoState.Instance.OnDisconnect += ResetProxies;
            //instance = this;
        }

        /// <summary>
        /// Forces this classes' proxy properties to refresh.
        /// </summary>
        private void ResetProxies(string ip, int port)
        {
            Motion = null;
            Posture = null;
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
            Posture.goToPosture("Stand", 1f);
        }

        /// <summary>
        /// Have the Nao sit-down.
        /// </summary>
        public void SitDown()
        {
            Posture.goToPosture("Sit", 0.5f);
        }

        /// <summary>
        /// Have the Nao crouch.
        /// </summary>
        public void Crouch()
        {
            Posture.goToPosture("Crouch", 0.5f);
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

            Motion.angleInterpolationWithSpeed(kneelNames, angles, 0.3F);
        }

        /// <summary>
        /// Move the nao head forwards or backwards.
        /// depth ranges from -0.5f to 0.5f.
        /// here negative value makes the nao look up, positive values make nao look down.
        /// heigth = 0f makes the nao look forwards.
        /// </summary>
        /// <param name="depth"></param>
        public void Look(float depth)
        {
            if (depth < -0.5f) depth = -0.5f;
            if (depth > 0.5f) depth = 0.5f;
            Logger.Log(this, "Look: "+depth);
            // avoid motors grinding, there has to be a difference
            if (depth != lastDepth)
            {
                lastDepth = depth;
                Motion.angleInterpolationWithSpeed(
                    new ArrayList(new string[] { "HeadPitch" }), new ArrayList(new float[] { depth }), 0.1f);
            }
        }

        public bool Balanced
        {
            get
            {
                if (!IsStable()) 
                    AttemptStabilize();
                return IsStable();
            }
        }

        private static string format(float f)
        {
            string res = f + "";
            if (res.Length < 5) return res;
            else return res.Substring(0, 5);
        }

        private List<float> Angles(ArrayList names, bool reverseRolls)
        {
            List<float> angles = Motion.getAngles(names, false);
            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].ToString().Contains("Roll"))
                {
                    angles[i] = -angles[i];
                }
            }
            return angles;
        }

        private List<float> LeftAngles(ArrayList names)
        {
            return Angles(names, true);
        }

        private List<float> RightAngles(ArrayList names)
        {
            return Angles(names, false);
        }

        public bool IsStable()
        {
            if (ignoreStabalise) return false;
            List<float> rAngles = RightAngles(rLegNames);
            List<float> lAngles = LeftAngles(lLegNames);
            Logger.Log(this, rAngles.Count + " - " + lAngles.Count);
            bool stable = true;
            for (int i = 0; i < 6; i++)
            {
                float reverse = lLegNames[i].ToString().Contains("Roll") ? -1f : 1f;
                Logger.Log(this, lLegNames[i] + ": " + format(lAngles[i]) + " - " + format(rAngles[i]) + " Diff = " + format(lAngles[i] - rAngles[i]));
                stable = stable && Math.Abs(lAngles[i] - rAngles[i]) < maxAllowedDifference;
            }
            Logger.Log(this, "Stable: " + stable);
            return stable;
        }

        /*
         * Attempt to stabalise the robot if the angles dont differ to much.
         */
        private bool AttemptStabilize()
        {
            if (ignoreStabalise) return false;
            Logger.Log(this, "AttemptStabilize");

            // don't stabilize to much
            if (lastStabiliseAttempt + 50000000 /*nanoseconds*/ < DateTime.Now.Ticks)
            {
                Logger.Log(this, (DateTime.Now.Ticks - lastStabiliseAttempt) + "");
                lastStabiliseAttempt = DateTime.Now.Ticks;
                List<float> left = Angles(lLegNames, false);
                List<float> right = Angles(rLegNames, false);
                ArrayList angles = new ArrayList();
                FillDataArray(angles, left, right);
                if (angles.Count == 6)
                {
                    Logger.Log(this, "Stabilising");
                    ArrayList names = new ArrayList(lLegNames);
                    names.AddRange(rLegNames);
                    angles.AddRange(angles);
                    Motion.setAngles(names, angles, 0.1f);
                }
                return angles.Count == 6;
            }
            return false;
        }

        private void FillDataArray(ArrayList angles, List<float> left, List<float> right)
        {
            for (int i = 0; i < 6; i++)
            {
                Logger.Log(this, lLegNames[i] + ": " + format(left[i]) + " - " + format(right[i]) + " Diff = " + format(left[i] - right[i]));
                if (Math.Abs(left[i] - right[i]) < attemptStabaliseLimit)
                {
                    float avg = (left[i] + right[i]) / 2;
                    if (lLegNames[i].ToString().Contains("Roll"))
                        avg = -avg;
                    angles.Add(avg);
                }
            }
        }
    }
}
