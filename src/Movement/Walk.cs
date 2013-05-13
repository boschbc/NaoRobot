﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

using Aldebaran.Proxies;

using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    class Walk
    {
        private MotionProxy motion; //deprecated
        private RobotPostureProxy posture; //deprecated

        private static Walk instance = null;

        private int markerID = -1;
        private Thread t = null;
        private Boolean found = false;

        public Walk()
        {
            motion = NaoState.MotionProxy;
            posture = NaoState.PostureProxy;
        }

        public static Walk Instance
        {
            get {
                if (instance == null)
                {
                    instance = new Walk();
                }
                return instance;
            }
        }

        /**
         * walk to (x, y, theta) with the Nao as the origin
         * */
        public void WalkTo(float x, float y, float theta)
        {
            InitMove();
            using (MotionProxy motion = NaoState.MotionProxy)
            {
                if (!motion.moveIsActive())
                    motion.moveInit();
                motion.moveTo(x, y, theta);
                //motion.stopMove();
            }
        }

        /**
         * Sets the stiffness of the Nao's motors on if it is not already so.
         **/
        public void InitMove()
        {
            using (MotionProxy motion = NaoState.MotionProxy)
            {
                if (!motion.robotIsWakeUp())
                    motion.wakeUp();
            }
        }

        /**
         * Start walking with normalized speed x, y and theta
         * */
        public void StartWalking(float x, float y, float theta)
        {
            if (!IsMoving()) 
                motion.moveInit();
            motion.moveToward(x, y, theta);
        }

        /**
         * Turn (normalized) dir and walk till the Nao sees the marker with MarkID = markerID
         * */
        public void WalkTowards(float dir, int markerID)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            this.markerID = markerID;
            found = false;
            
            t = new Thread(new ThreadStart(LookForMarker));
            t.Start();
        }

        /**
         * Try to detect the marker with MarkID = markerID.
         * When the Nao sees the marker, it heads towards the marker.
         * When the Nao is within a meter of the marker, the Nao stops moving and found is set to true
         * */
        public void LookForMarker()
        {
            MarkerRecogniser rec = MarkerRecogniser.GetInstance();
            Sonar sonar = Sonar.Instance;
            ArrayList markers;
            sonar.ActivateSonar();

            try
            {
                while (!found)
                {
                    ArrayList data = rec.GetMarkerData();
                    markers = data.Count == 0 ? data : (ArrayList)data[1];
                    Console.WriteLine("New Loop " + markers.Count);
                    if (markers.Count == 0)
                    {
                        // Temp fix
                        StopMove();
                        return;
                    }
                    for (int i = 0; i < markers.Count && !found; i++)
                    {
                        ArrayList marker = (ArrayList)markers[i];
                        if ((int)((ArrayList)marker[1])[0] == markerID)
                        {
                            float angle = ((float)((ArrayList)marker[0])[1]) / 4F;
                            StartWalking(0.5F, 0, Math.Max(-1, Math.Min(1, angle)));

                            Console.WriteLine("Alpha = " + ((float)((ArrayList)marker[0])[1]));
                            Console.WriteLine("Angle = " + angle);
                            float target = 0.40f;
                            float sonarL = sonar.getSonarDataLeft();
                            float sonarR = sonar.getSonarDataRight();
                            Console.WriteLine("SonarL = " + sonarL);
                            Console.WriteLine("SonarR = " + sonarR);

                            Console.WriteLine("TestL = " + (sonarL - target) + " < " + 0.001);
                            Console.WriteLine("TestR = " + (sonarR - target) + " < " + 0.001);
                            Console.WriteLine("Markerfound");

                            if (sonarL >= 0.20f && sonarL - target < 0.001 && sonarR >= 0.20f && sonarR - target < 0.001)
                            {
                                Console.WriteLine("stopping");
                                Console.WriteLine("SonarL was = " + sonarL);
                                Console.WriteLine("SonarR was = " + sonarR);
                                StopMove();
                                found = true;
                            }
                        }
                    }
                    Thread.Sleep(250);
                }
                Console.WriteLine("Exit LookForMarker");
            }
            finally
            {
                sonar.Deactivate();
            }
        }

        /**
         * return found
         * found is true when the marker with MarkID = markerID has been found and reached
         * */
        public Boolean IsFound()
        {
            return found;
        }

        /**
         * stop looking for the marker and stop moving
         * */
        public void StopLooking()
        {
            if (t != null)
            {
                t.Abort();
                t = null;
            }
            StopMove();
        }

        /** 
         * return true iff the Nao is moving
         * */
        public Boolean IsMoving()
        {
            try
            {
                return motion.moveIsActive();
            }
            catch
            {
                return true;
            }
        }

        /**
         * stop the Nao from moving
         * */
        public void StopMove()
        {
            motion.stopMove();
        }
    }
}
