using System;

using Aldebaran.Proxies;

using Naovigate.Util;

namespace Naovigate.Movement
{
    /// <summary>
    /// A class which manages and controls the Nao's movements.
    /// </summary>
    public sealed class Walk : IDisposable
    {
        private static Walk instance = null;

        /// <summary>
        /// Returns this singleton's instance.
        /// </summary>
        public static Walk Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Walk();
                }
                return instance;
            }
            set { instance = value; }
        }

        /// <summary>
        /// Converts a given radians into a radian in [-pi, pi]
        /// </summary>
        /// <param name="rad">An angel in radians.</param>
        /// <returns>A new angle in [-pi, pi], in radians.</returns>
        private static float ToNaoRadians(float rad)
        {
            if (Math.Abs(rad) > Math.PI)
            {
                rad %= (float)Math.PI;
                if (rad > 0)
                    rad -= (float)(Math.PI);
                else if (rad < 0)
                    rad += (float)(Math.PI);
            }
            return rad;
        }

        private MotionProxy motion;

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
        /// Default constructor.
        /// </summary>
        public Walk()
        {
            NaoState.Instance.OnDisconnect += ResetProxies;
            NaoState.Instance.OnConnect += (ip, port) => North = NaoState.Instance.Rotation;
            if (NaoState.Instance.Connected)
                North = NaoState.Instance.Rotation;
        }

        /// <summary>
        /// The direction defined as North, in radians.
        /// </summary>
        public float North
        {
            get;
            private set;
        }

        /// <summary>
        /// The direction defined as South in radians.
        /// </summary>
        public float South
        {
            get { return (float)(North + Math.PI); }
        }

        /// <summary>
        /// The direction defined as East in radians.
        /// </summary>
        public float East
        {
            get { return (float)(North + 0.5 * Math.PI); }
        }

        /// <summary>
        /// The direction defined as West in radians.
        /// </summary>
        public float West
        {
            get { return (float)(North - 0.5 * Math.PI); }
        }

        /// <summary>
        /// Forces this classes' proxy properties to refresh.
        /// </summary>
        private void ResetProxies(string ip, int port)
        {
            Motion = null;
        }

        /// <summary>
        /// Sets the stiffness of the Nao's motors on if it is not already so.
        /// </summary>
        public void InitMove()
        {
            if (!Motion.robotIsWakeUp())
                Motion.wakeUp();
            if (!Motion.moveIsActive())
                Motion.moveInit();
        }

        /// <summary>
        /// Blocks the thread until the Nao is not moving.
        /// </summary>
        public void WaitForMoveToEnd()
        {
            Motion.waitUntilMoveIsFinished();
        }

        /// <summary>
        /// The Nao will walk towards given coordinates.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        /// <param name="theta">The angle with which to move towards the destination.</param>
        public void WalkTo(float x, float y, float theta)
        {
            InitMove();
            Motion.post.moveTo(x, y, theta);
        }

        /// <summary>
        /// Prepares the Nao's posture to be able to walk while holding an object.
        /// </summary>
        public bool WalkWithObject
        {
            set
            {
                if (value)
                    Motion.setWalkArmsEnable(false, false);
                else
                    Motion.setWalkArmsEnable(true, true);
            }
        }

        /// <summary>
        /// Turn the Nao relatively.
        /// This method may be inaccurate at times.
        /// </summary>
        public void Turn(float rad)
        {
            WalkTo(0, 0, rad);
            WaitForMoveToEnd();
        }

        /// <summary>
        /// Turns the Nao accurately using a default accuracy
        /// </summary>
        /// <param name="rad">The angle to turn in radians. A positive value means clockwise rotation.</param>
        public void TurnExact(float rad)
        {
            TurnExact(rad, 0.01f);
        }
        /// <summary>
        /// Turns the Nao accurately using the specified accuracy-degree.
        /// </summary>
        /// <param name="rad">The angle to turn in radians. A positive value means clockwise rotation.</param>
        /// <param name="accuracy">How accurately to turn.</param>
        public void TurnExact(float rad, float accuracy)
        {
            if (accuracy < 0.01)
                accuracy = 0.01f;

            NaoState.Instance.Update();
            
            float goal = ToNaoRadians(NaoState.Instance.Rotation + rad);
            float rotation = ToNaoRadians(goal - NaoState.Instance.Rotation);
            float mistake = Math.Abs(rotation);
            while (mistake > accuracy)
            {
                Turn(rotation);
                NaoState.Instance.Update();
                rotation = ToNaoRadians(goal - NaoState.Instance.Rotation);
                mistake = Math.Abs(rotation);
            }
        }

        /// <summary>
        /// Turns the Nao absolutely.
        /// </summary>
        /// <param name="rad">The angle to turn to, in radians.</param>
        /// <param name="accuracy">
        /// If greater than 0, will turn exactly 
        /// using given accuracy as a threshold.
        /// </param>
        public void TurnAbsolute(float rad, float accuracy)
        {
            NaoState.Instance.Update();
            float delta = rad - NaoState.Instance.Rotation;
            if (accuracy > 0)
                TurnExact(delta, accuracy);
            else
                Turn(delta);
        }

        /// <summary>
        /// Turns the Nao absolutely.
        /// </summary>
        /// <param name="rad">The angle to turn to, in radians.</param>
        public void TurnAbsolute(float rad)
        {
            TurnAbsolute(rad, 0);
        }

        /// <summary>
        /// Start walking with normalized speed x, y and theta
        /// </summary>
        /// <param name="x">Speed along the X-axis.</param>
        /// <param name="y">Speed along the Y-axis.</param>
        /// <param name="theta">The angle with the Nao will be in while moving.</param>
        public void StartWalking(float x, float y, float theta)
        {
            InitMove();
            Motion.moveToward(x, y, theta);
        }

        /// <summary>
        /// Turn (normalized) dir and walk till the Nao is within dist pieces of wall of the marker with MarkID = markerID
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="markerID"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public MarkerSearchWorker WalkTowardsMarker(float dir, int markerID, int dist)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.7F, 0, 0);
            MarkerSearchWorker t = new MarkerSearchWorker(markerID,dist);
            t.Start();
            return t;
        }

        /// <summary>
        /// The Nao will stop looking for markers and stop moving.
        /// </summary>
        public void StopLooking()
        {
            StopMoving();
        }

        /// <summary>
        /// Returns true iff the Nao is moving.
        /// </summary>
        /// <returns>A boolean.</returns>
        public Boolean IsMoving()
        {
            try
            {
                return Proxies.GetProxy<MotionProxy>().moveIsActive();
            }
            catch(Exception e)
            {
                Logger.Log(this, e.GetType() + " : " + e.Message);
                if (e.Message.Contains("Access")) return false;
                // proxy is busy (moving) so return true
                else return true;
                
            }
        }

        /// <summary>
        /// Stops the Nao from moving.
        /// </summary>
        public void StopMoving()
        {
            Logger.Log(this, "Stopping...");
            Motion.post.stopMove();
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            if (motion != null)
                motion.Dispose();
        }
    }
}
