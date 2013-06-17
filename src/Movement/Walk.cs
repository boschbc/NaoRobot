using System;
using Aldebaran.Proxies;
using Naovigate.Navigation;
using Naovigate.Util;

namespace Naovigate.Movement
{
    /// <summary>
    /// A class which manages and controls the Nao's movements.
    /// </summary>
    public sealed class Walk : IDisposable
    {
        private static Walk instance = null;
        private Direction currentDirection = Direction.Up;
        
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
            if (NaoState.Instance.Connected)
            {
                WalkWithObject = true;
            }
        }


        /// <summary>
        /// Forces this classes' proxy properties to refresh.
        /// </summary>
        private void ResetProxies(string ip, int port)
        {
            Motion = null;
        }

        public bool MotorOn()
        {
            return Motion.robotIsWakeUp();
        }

        /// <summary>
        /// Sets the stiffness of the Nao's motors on if it is not already so.
        /// </summary>
        public void InitMove()
        {
            if (!MotorOn())
                Motion.wakeUp();
            if (!Motion.moveIsActive())
                Motion.moveInit();
        }

        /// <summary>
        /// Blocks the thread until the Nao is not moving.
        /// </summary>
        public void WaitForMoveToEnd()
        {
            //Logger.Log(this, "WaitForMove");
            Motion.waitUntilMoveIsFinished();
            //Logger.Log(this, "MoveFinished");
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
            Motion.moveTo(x, y, theta);
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
        /// Turn towards the given direction
        /// </summary>
        /// <param name="dir"></param>
        public void TurnTo(Direction dir)
        {
            Logger.Say("Turn To "+dir.ToString());
            //calculate how to get to up
            int turn = 4 - (int)currentDirection;
            //and adjust turn to turn to dir
            turn += (int)dir;
            turn = turn % 4;
            switch ((Direction)turn)
            {
                case Direction.Left :
                    TurnLeft();
                    break;
                case Direction.Right :
                    TurnRight();
                    break;
                case Direction.Down :
                    TurnAround();
                    break;
            }
        }

        /// <summary>
        /// Turns 90 degrees to the left
        /// </summary>
        public void TurnLeft()
        {
            InitMove();
            motion.moveTo(0, 0, (float)(0.5 * Math.PI));
            int newDir = (int)currentDirection;
            newDir = (newDir + 3) % 4;
            currentDirection = (Direction)newDir;
        }

        /// <summary>
        /// Turns 90 degrees to the right
        /// </summary>
        public void TurnRight()
        {
            InitMove();
            //Because the Naos are broken, it's 0.4 instead of 0.5
            motion.moveTo(0,0,-(float)(0.4*Math.PI));
            int newDir = (int)currentDirection;
            newDir = (newDir + 1) % 4;
            currentDirection = (Direction)newDir;
        }

        /// <summary>
        /// Turn 180 degrees
        /// </summary>
        public void TurnAround()
        {
            TurnLeft();
            TurnLeft();
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
        /// Walk till the Nao is within dist pieces of wall of the marker with MarkID = markerID
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="markerID"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public MarkerSearchWorker WalkTowardsMarker(int markerID, int dist)
        {
            Logger.Log(this, "WalkToMarker: "+markerID+":"+dist);
            Logger.Say("To Marker "+markerID+" at "+dist);
            return new MarkerSearchWorker(markerID, dist);
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
