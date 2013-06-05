using System;
using System.Threading;
using System.Collections;
using Naovigate.Util;
using Naovigate.Movement;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    /*
     * Try to detect the marker with MarkID = markerID.
     * When the Nao sees the marker, it heads towards the marker.
     * When the Nao is within dist pieces of wall of the marker, the Nao stops moving and found is set to true
     */
    public class MarkerSearchThread : ActionExecutor
    {
        private int markerID;
        private double dist;
        private bool stoppedMyself = false;
        private float headPos = 0f;

        public MarkerSearchThread(int markerID, double dist)
        {
            this.markerID = markerID;
            this.dist = dist;
        }

        public override void Run()
        {
            running = true;
            try
            {
                LookForMarker();
            }
            finally
            {
                Pose.Instance.Look(0f);
            }
        }

        public void LookForMarker()
        {
            Pose.Instance.Look(headPos);
            MarkerRecogniser rec = MarkerRecogniser.GetInstance();
            ArrayList markers;
            Logger.Log(this, "Look for marker");
            Call(() => Walk.Instance.StartWalking(0.5F, 0, 0));
            while (running)
            {
                Thread.Sleep(1000);
				if (!stoppedMyself && !Walk.Instance.IsMoving()) running = false;
                ArrayList data = rec.GetMarkerData();
                markers = data.Count == 0 ? data : (ArrayList)data[1];
                if (markers.Count == 0)
                {
                    Logger.Log(this, "Temp stop, look for markers.");
                    headPos += 0.1f;
                    Logger.Log(this, "Head: "+headPos);
                    Call(() => Pose.Instance.Look(headPos));
                    stoppedMyself = true;
                    Walk.Instance.StopMove();
                    if (headPos > 0.5f)
                    {

                        Logger.Log(this, "NO MARKERS - CANT FIND MORE - EXIT PLEASE");
                        Pose.Instance.Look(0f);
                        Abort();
                    }
                }
                else
                {
                    checkMarkers(markers);
                }
            }
            Walk.Instance.StopMove();
            Pose.Instance.Look(0f);
            Logger.Log("Exit LookForMarker : "+running);
        }

        private void checkMarkers(ArrayList markers)
        {
            Logger.Log(this, "checkMarkers: "+markers.Count);
            for (int i = 0; i < markers.Count; i++)
            {
                Logger.Log(this, "marker: " + i);
                ArrayList marker = (ArrayList)markers[i];
                if ((int)((ArrayList)marker[1])[0] == markerID)
                {
                    Logger.Log(this, "Correct marker: " + running);
                    running = calculate(marker) ? false : running;
                    break;
                }
            }
        }

        //Change direction towards the marker and return true iff we reached our destination
        private bool calculate(ArrayList marker)
        {
            Logger.Log(this, "Calculate: "+running);
            bool reached = false;
            float angle = ((float)((ArrayList)marker[0])[1]) / 4F;
            if (running)
            {
                Logger.Log(this, "StartWalking: "+running);
                Call(() => Walk.Instance.StartWalking(0.5F, 0, Math.Max(-1, Math.Min(1, angle))));
                Call(() => stoppedMyself = false);
            }
            float sizeY = ((float)((ArrayList)marker[0])[4]);

            if (MarkerRecogniser.estimateDistance(sizeY) <= dist)
            {
                reached = true;
            }
            // look forward
            Pose.Instance.Look(0f);
            return reached;
        }
    }    
}
