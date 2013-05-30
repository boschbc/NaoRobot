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

            while (running)
            {
                Thread.Sleep(1000);
				if (!Walk.Instance.IsMoving()) running = false;
                ArrayList data = rec.GetMarkerData();
                markers = data.Count == 0 ? data : (ArrayList)data[1];
                checkMarkers(markers);
            }
            Walk.Instance.StopMove();
            Console.WriteLine("Exit LookForMarker");
        }

        private void checkMarkers(ArrayList markers)
        {
            for (int i = 0; i < markers.Count; i++)
            {
                ArrayList marker = (ArrayList)markers[i];
                if ((int)((ArrayList)marker[1])[0] == markerID)
                {
                    running = calculate(marker) ? false : running;
                    break;
                }
            }
        }

        //Change direction towards the marker and return true iff we reached our destination
        private bool calculate(ArrayList marker)
        {
            bool reached = false;
            float angle = ((float)((ArrayList)marker[0])[1]) / 4F;
            if (running)
            {
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
