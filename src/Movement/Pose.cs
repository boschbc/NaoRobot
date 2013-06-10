using System;
using System.Collections;
using System.Collections.Generic;
using Aldebaran.Proxies;
using Naovigate.Util;

namespace Naovigate.Movement
{
    internal sealed class Pose : IDisposable
    {
        private static readonly string UnusableProxiesMessage = "A Nao-proxy used by this class is either null or has been disposed of.";
        private static readonly bool ignoreStabilise = true;
        private static readonly bool ignoreIsStable = false;
        private static readonly float maxAllowedDifference = 0.3f;
        private static readonly float attemptStabaliseLimit = 0.3f;

        private static readonly ArrayList rLegNames = new ArrayList(new string[] {"RHipYawPitch", "RHipRoll", "RHipPitch", "RKneePitch", "RAnklePitch", "RAnkleRoll"});
        private static readonly ArrayList lLegNames = new ArrayList(new string[] { "LHipYawPitch", "LHipRoll", "LHipPitch", "LKneePitch", "LAnklePitch", "LAnkleRoll" });
        private static readonly ArrayList kneelNames = new ArrayList(new String[] { "LHipPitch", "RHipPitch", "LKneePitch", "RKneePitch", "LAnklePitch", "RAnklePitch" });

        
        private static Pose instance;
        private long lastStabiliseAttempt = DateTime.Now.Ticks;
        
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
        /// Constructor.
        /// </summary>
        public Pose()
        {
            NaoState.Instance.OnConnect += BuildProxies;
            NaoState.Instance.OnDisconnect += ResetProxies;
            NaoState state = NaoState.Instance;
            if (state.Connected)
            {
                BuildProxies(state.IP.ToString(), state.Port);
            }
        }

        /// <summary>
        /// Fetches new proxies to be used by this class.
        /// </summary>
        /// <param name="ip">The IP of the Nao connected to.</param>
        /// <param name="port">The port number the Nao connected to.</param>
        private void BuildProxies(string ip, int port)
        {
            motion = Proxies.GetProxy<MotionProxy>();
            posture = Proxies.GetProxy<RobotPostureProxy>();
        }

        /// <summary>
        /// Discards the proxies used by this class (intended to be called when they have been disposed).
        /// </summary>
        /// <param name="ip">The IP of the Nao disconnected from.</param>
        /// <param name="port">The port number of the Nao disconnected from.</param>
        private void ResetProxies(string ip, int port)
        {
            motion = null;
            posture = null;
        }

        /// <summary>
        /// Checks if the proxies used by this class are non-null and un-disposed.
        /// </summary>
        /// <returns>A boolean indicating whether the proxies used by this class are usable.</returns>
        private bool ProxiesAreUsable()
        {
            return motion != null && posture != null;
        }

        /// <summary>
        /// Checks whether the proxes used by this class are not in a corrupted state.
        /// </summary>
        /// <exception cref="InvalidOperationException">One or more proxies are either null or were disposed of.</exception>
        private void ValidateProxies()
        {
            if (!ProxiesAreUsable())
                throw new InvalidOperationException(UnusableProxiesMessage);
        }

        /// <summary>
        /// Have the Nao stand up.
        /// </summary>
        public void StandUp()
        {
            ValidateProxies();
            posture.goToPosture("Stand", 1f);
        }

        /// <summary>
        /// Have the Nao sit-down.
        /// </summary>
        public void SitDown()
        {
            ValidateProxies();
            posture.goToPosture("Sit", 0.5f);
        }

        /// <summary>
        /// Have the Nao crouch.
        /// </summary>
        public void Crouch()
        {
            ValidateProxies();
            posture.goToPosture("Crouch", 0.5f);
        }

        /// <summary>
        /// Make the Nao kneel, with specified depth.
        /// </summary>
        /// <param name="depth">0 means standing. 1 means sitting.</param>
        public void Kneel(float depth)
        {
            ValidateProxies();
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

        /// <summary>
        /// Move the nao head forwards or backwards.
        /// depth ranges from -0.5f to 0.5f.
        /// here negative value makes the nao look up, positive values make nao look down.
        /// heigth = 0f makes the nao look forwards.
        /// </summary>
        /// <param name="depth"></param>
        public void Look(float depth)
        {
            ValidateProxies();
            if (depth < -0.5f) depth = -0.5f;
            if (depth > 0.5f) depth = 0.5f;
            Logger.Log(this, "Look: "+depth);
            // avoid motors grinding, there has to be a difference
            if (depth != lastDepth)
            {
                lastDepth = depth;
                motion.angleInterpolationWithSpeed(
                    new ArrayList(new string[] { "HeadPitch" }), new ArrayList(new float[] { depth }), 0.1f);
            }
        }

        /// <summary>
        /// True if the Nao's posture is currently balanced.
        /// May attempt to stabilize the Nao in the process.
        /// </summary>
        public bool Balanced
        {
            get
            {
                if (!IsStable()) 
                    AttemptStabilize();
                return IsStable();
            }
        }

        /// <summary>
        /// Takes a list of joints and reverses the value of any "Roll" joints and returns the resulting list of values as floats.
        /// </summary>
        /// <param name="names">A list of body-part names as defined by the NaoQI API.</param>
        /// <returns>A list of floating point numbers representing the reversed rolls.</returns>
        private List<float> Angles(ArrayList names)
        {
            ValidateProxies();
            List<float> angles = motion.getAngles(names, false);
            for (int i = 0; i < names.Count; i++)
            {
                if (names[i].ToString().Contains("Roll"))
                {
                    angles[i] = -angles[i];
                }
            }
            return angles;
        }

        /// <summary>
        /// Checks whether the Nao's posture is stable.
        /// </summary>
        /// <returns>True if the Nao is stable, false otherwise.</returns>
        public bool IsStable()
        {
            if (ignoreIsStable) 
                return false;

            List<float> rAngles = Angles(rLegNames);
            List<float> lAngles = Angles(lLegNames);
            Logger.Log(this, rAngles.Count + " - " + lAngles.Count);
            bool stable = true;
            for (int i = 0; i < 6; i++)
            {
                //float reverse = lLegNames[i].ToString().Contains("Roll") ? -1f : 1f;
                float dif = lAngles[i] - rAngles[i];
                Logger.Log(this, lLegNames[i] + ": " + lAngles[i].Readable() + " - " + rAngles[i].Readable() + " Diff = " + dif.Readable());
                stable = stable && Math.Abs(dif) < maxAllowedDifference;
            }
            Logger.Log(this, "Stable: " + stable);
            return stable;
        }

        /// <summary>
        /// Attempts to stabalise the Nao if the joint-angles do not differ too much.
        /// </summary>
        /// <returns>True if stabilization attempt succeded, false otherwise.</returns>
        private bool AttemptStabilize()
        {
            if (ignoreStabilise)
                return false;

            Logger.Log(this, "Attemping to stabilize...");
            List<float> left;
            List<float> right;
            ArrayList angles;
            ArrayList names;

            if (lastStabiliseAttempt + 50000000 /*nanoseconds*/ < DateTime.Now.Ticks)
            {
                Logger.Log(this, "Last stabilization attempt was at: " + (DateTime.Now.Ticks - lastStabiliseAttempt));
                lastStabiliseAttempt = DateTime.Now.Ticks;
                left = Angles(lLegNames);
                right = Angles(rLegNames);
                angles = new ArrayList();
                FillDataArray(angles, left, right);
                if (angles.Count == 6)
                {
                    Logger.Log(this, "Stabilizing now...");
                    names = new ArrayList(lLegNames);
                    names.AddRange(rLegNames);
                    angles.AddRange(angles);
                    motion.setAngles(names, angles, 0.1f);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// No idea what this does.
        /// </summary>
        /// <param name="angles">?</param>
        /// <param name="left">?</param>
        /// <param name="right">?</param>
        private void FillDataArray(ArrayList angles, List<float> left, List<float> right)
        {
            for (int i = 0; i < 6; i++)
            {
                Logger.Log(this, lLegNames[i] + ": " + left[i].Readable() + " - " + right[i].Readable() + " Diff = " + (left[i] - right[i]).Readable());
                if (Math.Abs(left[i] - right[i]) < attemptStabaliseLimit)
                {
                    float avg = (left[i] + right[i]) / 2;
                    if (lLegNames[i].ToString().Contains("Roll"))
                        avg = -avg;
                    angles.Add(avg);
                }
            }
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            if (motion != null)
                motion.Dispose();
            if (posture != null)
                posture.Dispose();
        }
    }

    
}
