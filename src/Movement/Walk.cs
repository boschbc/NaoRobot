using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

using Aldebaran.Proxies;

using Naovigate.Util;
using Naovigate.Vision;
using System.Drawing;

namespace Naovigate.Movement
{
    /// <summary>
    /// A class which manages and controls the Nao's movements.
    /// </summary>
    public class Walk
    {
        private MotionProxy motion; 
        private RobotPostureProxy posture; 

        private static Walk instance = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Walk()
        {
            motion = NaoState.Instance.MotionProxy;
            posture = NaoState.Instance.PostureProxy;
            instance = this;
        }

        /// <summary>
        /// Returns this singleton's instance.
        /// </summary>
        public static Walk Instance
        {
            get {
                if (instance == null)
                {
                    instance = new Walk();
                }
                return instance;
            }
            set { instance = value; }
        }

        /// <summary>
        /// Sets the stiffness of the Nao's motors on if it is not already so.
        /// </summary>
        public void InitMove()
        {
            if (!motion.robotIsWakeUp())
                motion.wakeUp();
            if (!motion.moveIsActive())
                motion.moveInit();
        }

        /// <summary>
        /// Blocks the thread until the Nao is not moving.
        /// </summary>
        public void WaitForMoveToEnd()
        {
            motion.waitUntilMoveIsFinished();
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
            motion.post.moveTo(x, y, theta);
        }

        /// <summary>
        /// Prepares the Nao's posture to be able to walk while holding an object.
        /// </summary>
        public bool WalkWithObject
        {
            set
            {
                if (value) motion.setWalkArmsEnable(false, false);
                else motion.setWalkArmsEnable(true, true);
            }
        }

        /// <summary>
        /// Turn the Nao.
        /// This method may be inaccurate at times.
        /// </summary>
        public void Turn(float rad)
        {
            WalkTo(0, 0, rad);
            WaitForMoveToEnd();
        }

        private float Normalize(float rad)
        {
            rad %= (float)Math.PI;
            if (rad > Math.PI)
                rad -= (float)Math.PI;
            if (rad < -Math.PI)
                rad += (float)Math.PI;
            return rad;
        }

        /// <summary>
        /// Turns the Nao accurately using the specified accuracy-degree.
        /// </summary>
        /// <param name="rad">The angle to turn in radians. A positive value means clockwise rotation.</param>
        /// <param name="accuracy">How accurately to turn.</param>i
        public void TurnExact(float rad, float accuracy)
        {
            if (accuracy < 0.01)
                accuracy = 0.01f;
            NaoState.Instance.Update();
            rad = Normalize(rad);
            Logger.Log(this, rad);
            float goal = Normalize(NaoState.Instance.Rotation + rad);
            float rotation = Normalize(goal - NaoState.Instance.Rotation);
            float mistake = Math.Abs(rotation);
            while (mistake > accuracy)
            {
                Logger.Log(this, "naoRotation: " + NaoState.Instance.Rotation);
                Logger.Log(this, "goalRotation: " + goal);
                Logger.Log(this, "helperRotation: " + rotation);
                Turn(rotation);
                NaoState.Instance.Update();
                rotation = Normalize(goal - NaoState.Instance.Rotation);
                mistake = Math.Abs(rotation);
            }
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
            motion.moveToward(x, y, theta);
        }

        /// <summary>
        /// Turn (normalized) dir and walk till the Nao is within dist pieces of wall of the marker with MarkID = markerID
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="markerID"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public MarkerSearchThread WalkTowardsMarker(float dir, int markerID, int dist)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            MarkerSearchThread t = new MarkerSearchThread(markerID,dist);
            t.Start();
            return t;
        }

        /// <summary>
        /// Turn (normalized) dir and walk till the Nao is within dist pieces of wall of the object with id ObjectID
        /// </summary>
        /// <param name="dir">???</param>
        /// <param name="objectID"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        //public virtual ObjectSearchThread WalkTowardsObject(float dir,int objectID,double dist)
        //{
        //    StopLooking();
        //    WalkTo(0, 0, dir);
        //    StartWalking(0.5F, 0, 0);
        //    ObjectSearchThread t = new ObjectSearchThread(objectID , dist);
        //    t.Start();
        //    return t;
        //}

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
                return motion.moveIsActive();
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
            motion.post.stopMove();
        }
    }
}
