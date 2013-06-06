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
        private bool seenMarker = false;
        private float headPos = 0f;

        public MarkerSearchThread(int markerID, int dist)
        {
            this.markerID = markerID;
            this.dist = dist + 0.5;
        }

        public override void Run()
        {
            Running = true;
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
                    if (dist <= 1) Naovigate.Event.EventQueue.Goal.Post(new Naovigate.Event.NaoToGoal.SeeEvent(markerID, (int)(dist - 0.5)));
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
                    seenMarker = true;
                    bool reached = calculate(marker);
                    Running = reached ? false : Running;
                    if (reached) Naovigate.Event.EventQueue.Goal.Post(new Naovigate.Event.NaoToGoal.SeeEvent(markerID, (int)(dist-0.5)));
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
                reached = true;
            }
            return reached;
        }
    }
}
