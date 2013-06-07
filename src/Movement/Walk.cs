﻿using System;
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
                        motion = NaoState.Instance.MotionProxy;
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
                        posture = NaoState.Instance.PostureProxy;
                }
                else
                    posture = null;
                return posture;
            }
            set { posture = value; }
        }
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Walk()
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
        /// Turn the Nao.
        /// This method may be inaccurate at times.
        /// </summary>
        public void Turn(float rad)
        {
            WalkTo(0, 0, rad);
            WaitForMoveToEnd();
        }

        private float ToNaoRadians(float rad)
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
            
            float goal = ToNaoRadians(NaoState.Instance.Rotation + rad);
            float rotation = ToNaoRadians(goal - NaoState.Instance.Rotation);
            float mistake = Math.Abs(rotation);
            while (mistake > accuracy)
            {
                Logger.Log(this, NaoState.Instance.Rotation + " " + goal + " " + rotation + " " + mistake);
                Turn(rotation);
                NaoState.Instance.Update();
                rotation = ToNaoRadians(goal - NaoState.Instance.Rotation);
                mistake = Math.Abs(rotation);
            }
            Logger.Log(this, NaoState.Instance.Rotation + " " + goal + " " + rotation + " " + mistake); 
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
        public MarkerSearchThread WalkTowardsMarker(float dir, int markerID, int dist)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.7F, 0, 0);
            MarkerSearchThread t = new MarkerSearchThread(markerID,dist);
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
                return Motion.moveIsActive();
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
    }
}
