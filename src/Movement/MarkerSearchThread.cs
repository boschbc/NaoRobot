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

        public MarkerSearchThread(int markerID, double dist)
        {
            this.markerID = markerID;
            this.dist = dist;
        }

        public override void Run()
        {
            running = true;
            LookForMarker();
        }

        public void LookForMarker()
        {
            MarkerRecogniser rec = MarkerRecogniser.GetInstance();
            ArrayList markers;
            Logger.Log(this, "Look for marker");
            Call(() => Walk.Instance.StartWalking(0.5F, 0, 0));
            while (running)
            {
                Thread.Sleep(1000);
				if (!Walk.Instance.IsMoving()) running = false;
                ArrayList data = rec.GetMarkerData();
                markers = data.Count == 0 ? data : (ArrayList)data[1];
                checkMarkers(markers);
            }
            Walk.Instance.StopMove();
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
            }
            float sizeY = ((float)((ArrayList)marker[0])[4]);

            if (MarkerRecogniser.estimateDistance(sizeY) <= dist)
            {
                reached = true;
            }

            return reached;
        }
    }    
}
