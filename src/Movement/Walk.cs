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
        /// The Nao will walk towards given coordinates.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        /// <param name="theta">The angle with which to move towards the destination.</param>
        public void WalkTo(float x, float y, float theta)
        {
            InitMove();
            motion.moveTo(x, y, theta);
        }

        /// <summary>
        /// The Nao will adjust it's posture and walk as if holding an object.
        /// </summary>
        public void WalkWhileHolding()
        {
            motion.setWalkArmsEnable(false, false);
            motion.moveToward(0.8F, 0, 0);
            //motion.setWalkArmsEnable(true, true);
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

        public void MoveTo(float x, float y)
        {
            motion.moveTo(x, y, 0f);
        }

        public void MoveExact(float x, float y, float accuracy)
        {
            if (accuracy < 0.01)
                accuracy = 0.01f;
            NaoState.Instance.Update();
            PointF goal = new PointF(NaoState.Instance.Location.X + x,
                                     NaoState.Instance.Location.Y + y);
            PointF move = new PointF(goal.X - NaoState.Instance.Location.X,
                                     goal.Y - NaoState.Instance.Location.Y);
            PointF mistake = new PointF(Math.Abs(move.X), Math.Abs(move.Y));
            while (mistake.X > accuracy || mistake.Y > accuracy)
            {
                Logger.Log(this, "Current location: " + NaoState.Instance.Location);
                Logger.Log(this, "Goal: " + goal);
                Logger.Log(this, "Move: " + move);
                Logger.Log(this, "Mistake: " + mistake);
                MoveTo(move.X, move.Y);
                NaoState.Instance.Update();
                move.X = goal.X - NaoState.Instance.Location.X;
                move.Y = goal.Y - NaoState.Instance.Location.Y;
                mistake.X = Math.Abs(move.X);
                mistake.Y = Math.Abs(move.Y);
            }
            Logger.Log(this, "Current location: " + NaoState.Instance.Location);
            Logger.Log(this, "Goal: " + goal);
            Logger.Log(this, "Mistake: " + mistake);
        }
        /// <summary>
        /// Turn the Nao.
        /// This method may be inaccurate at times.
        /// </summary>
        public void Turn(float rad)
        {
            WalkTo(0, 0, rad);
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
            Logger.Log(this, "Rotating: " + rad);
            Logger.Log(this, "Accuracy: " + accuracy);
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
        public MarkerSearchThread WalkTowardsMarker(float dir, int markerID, double dist)
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
        public virtual ObjectSearchThread WalkTowardsObject(float dir,int objectID,double dist)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            ObjectSearchThread t = new ObjectSearchThread(objectID , dist);
            t.Start();
            return t;
        }

        /// <summary>
        /// The Nao will stop looking for markers and stop moving.
        /// </summary>
        public void StopLooking()
        {
            StopMove();
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
        public void StopMove()
        {
            motion.stopMove();
        }
    }
}
