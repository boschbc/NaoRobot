using System;
using System.Collections;
using System.Threading;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    /*
     * Try to detect the marker with MarkID = markerID.
     * When the Nao sees the marker, it heads towards the marker.
     * When the Nao is within dist pieces of wall of the marker, the Nao stops moving and found is set to true
     */
    public sealed class MarkerSearchThread : ActionExecutor
    {
        private int markerID;
        private double dist;
        private float headPos = 0f;

        public MarkerSearchThread(int markerID, int dist)
        {
            this.markerID = markerID;
            // distance to wall, so add 0.5 distance to effectively
            // end up in the middle of the room.
            this.dist = dist + 0.5;
        }

        public override void Run()
        {
            Running = true;
            try
            {
                Call(() => LookForMarker());
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
            Sonar sonar = Sonar.Instance;
            ArrayList markers;
            Logger.Log(this, "Look for marker");
            Call(() => Walk.Instance.StartWalking(0.5F, 0, 0));
            while (Running)
            {
                Thread.Sleep(1000);
                if (!Walk.Instance.IsMoving()) Running = false;
                ArrayList data = rec.GetMarkerData();
                markers = data.Count == 0 ? data : (ArrayList)data[1];
                checkMarkers(markers);
                if (markers.Count == 0 && sonar.IsTooClose())
                {
                    Logger.Log(this, "I probably reached the marker");
                    // wrong, See = object, were looking at markers here, need LocationEvent
                    //if (dist <= 1) EventQueue.Goal.Post(new SeeEvent(markerID, (int)(dist - 0.5)));
                    Running = false;
                }
            }
            Walk.Instance.StopMoving();
            Logger.Log("Exit LookForMarker : " + Running);
        }

        private void checkMarkers(ArrayList markers)
        {
            Logger.Log(this, "checkMarkers: " + markers.Count);
            for (int i = 0; i < markers.Count; i++)
            {
                Logger.Log(this, "marker: " + i);
                ArrayList marker = (ArrayList)markers[i];
                if ((int)((ArrayList)marker[1])[0] == markerID)
                {
                    Logger.Log(this, "Correct marker: " + Running);
                    bool reached = calculate(marker);
                    Running = reached ? false : Running;
                    // wrong, See = object, were looking at markers here, need LocationEvent
                    //if (reached) EventQueue.Goal.Post(new SeeEvent(markerID, (int)(dist-0.5)));
                    break;
                }
            }
        }

        //Change direction towards the marker and return true iff we reached our destination
        private bool calculate(ArrayList marker)
        {
            Logger.Log(this, "Calculate: " + Running);
            bool reached = false;
            float angle = ((float)((ArrayList)marker[0])[1]) / 4F;
            if (Running)
            {
                Logger.Log(this, "StartWalking: " + Running);
                Call(() => Walk.Instance.StartWalking(0.5F, 0, Math.Max(-1, Math.Min(1, angle))));
            }
            float sizeY = ((float)((ArrayList)marker[0])[4]);

            if (MarkerRecogniser.estimateDistance(sizeY) <= (double)dist)
            {
                Logger.Log("At Correct Distance: "+dist);
                reached = true;
            }
            return reached;
        }
    }
}
