using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

using Aldebaran.Proxies;

using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    class Walk
    {
        private MotionProxy motion;
        private RobotPostureProxy posture;

        private static Walk instance = null;

        private int markerID = -1;
        private Thread t = null;
        private Boolean found = false;

        public Walk()
        {
            motion = NaoState.GetMotionProxy();
            posture = NaoState.GetRobotPostureProxy();
        }

        public void RefreshProxies()
        {
            motion = NaoState.GetMotionProxy();
            posture = NaoState.GetRobotPostureProxy();
        }

        public static Walk GetInstance()
        {
            return instance == null ? new Walk() : instance;
        }

        /**
         * walk to (x, y, theta) with the Nao as the origin
         * */
        public void WalkTo(float x, float y, float theta)
        {
            //System.Console.WriteLine(x + " " + y);
            //posture.goToPosture("StandZero", 1f);
            //motion.moveInit();
            //motion.moveTo(x, y, theta);
            //motion.stopMove();
            if (!IsMoving()) 
                motion.moveInit();
            motion.moveTo(x, y, theta);
            motion.stopMove();
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
        public void walkTowards(float dir, int markerID)
        {
            StopLooking();
            WalkTo(0, 0, dir);
            StartWalking(0.5F, 0, 0);
            this.markerID = markerID;
            found = false;
            
            t = new Thread(new ThreadStart(LookForMarker));
        }

        /**
         * Try to detect the marker with MarkID = markerID.
         * When the Nao sees the marker, stop moving and set found to true
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
                        found = true;
                        StopMove();
                    }
                }
            }
        }

        /**
         * return found
         * found is true when the marker with MarkID = markerID has been found
         * */
        public Boolean IsFound()
        {
            return found;
        }

        /**
         * stop looking for the marker
         * */
        public void StopLooking()
        {
            if (t != null)
            {
                t.Abort();
                t = null;
            }
        }

        /** 
         * return true iff the Nao is moving
         * */
        public Boolean IsMoving()
        {
            return motion.moveIsActive();
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
