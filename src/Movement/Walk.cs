using System;
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
        private double dist = 1;

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
         * Turn (normalized) dir and walk till the Nao is within dist pieces of wall of the marker with MarkID = markerID
         * */
        public void WalkTowards(float dir, int markerID, double dist)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            this.markerID = markerID;
            found = false;
            this.dist = dist;
            
            t = new Thread(new ThreadStart(LookForMarker));
            t.Start();
        }

        /**
         * Try to detect the marker with MarkID = markerID.
         * When the Nao sees the marker, it heads towards the marker.
         * When the Nao is within dist pieces of wall of the marker, the Nao stops moving and found is set to true
         * */
        public void LookForMarker()
        {
            MarkerRecogniser rec = MarkerRecogniser.GetInstance();
            ArrayList markers;

                while (!found)
                {
                    ArrayList data = rec.GetMarkerData();
                    markers = data.Count == 0 ? data : (ArrayList)data[1];
                    for (int i = 0; i < markers.Count && !found; i++)
                    {
                        ArrayList marker = (ArrayList)markers[i];
                        if ((int)((ArrayList)marker[1])[0] == markerID)
                        {
                            float angle = ((float)((ArrayList)marker[0])[1]) / 4F;
                            StartWalking(0.5F, 0, Math.Max(-1, Math.Min(1, angle)));

                            float sizeY = ((float)((ArrayList)marker[0])[4]);

                            if (MarkerRecogniser.estimateDistance(sizeY) <= dist)
                            {
                                StopMove();
                                found = true;
                            }
                        }
                    }
                    Thread.Sleep(250);
                }
                Console.WriteLine("Exit LookForMarker");
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
