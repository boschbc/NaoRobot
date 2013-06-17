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
    public sealed class MarkerSearchWorker : ActionExecutor
    {
        private static readonly float speed = 0.7F;
        private int markerID;
        private double dist;
        private bool looking = true;

        public MarkerSearchWorker(int markerID, int dist)
        {
            this.markerID = markerID;
            // distance to wall, so add 0.5 distance to effectively
            // end up in the middle of the room.
            this.dist = dist + 0.5;
        }

        public override void Run()
        {
            try
            {
                Call(() => LookForMarker());
            }
            finally
            {
                Eyes.Instance.LookStraight();
            }
        }

        public void LookForMarker()
        {
            Pose.Instance.Look(0f);
            MarkerRecogniser rec = MarkerRecogniser.Instance;
            Sonar sonar = Sonar.Instance;
            ArrayList markers;
            Logger.Log(this, "Look for marker");
            Call(() => Walk.Instance.StartWalking(speed, 0, 0));
            while (Running && looking)
            {
                Thread.Sleep(1000);
                ArrayList data = rec.GetMarkerData();
                markers = data.Count == 0 ? data : (ArrayList)data[1];
                CheckMarkers(markers);
                // dont check sensor when holding object, it will probably block it.
                if (!NaoState.Instance.HoldingObject && markers.Count == 0)
                {
                    bool leftCollide, rightCollide, toClose;
                    toClose = sonar.IsTooClose(out leftCollide, out rightCollide);
                    if (toClose)
                    {
                        Logger.Log(this, "I probably reached the marker");
                        looking = false;
                    }
                    else if (leftCollide || rightCollide)
                    {
                        // turn a bit.
                        float dir = (float)(leftCollide ? -0.2f * Math.PI : 0.2f * Math.PI);
                        Walk.Instance.StopMoving();
                        Walk.Instance.Turn(dir);
                    }
                }
            }
            Walk.Instance.StopMoving();
            Logger.Log("Exit LookForMarker : " + Running);
        }

        /// <summary>
        /// check all the markers currently visible.
        /// </summary>
        /// <param name="markers"></param>
        private void CheckMarkers(ArrayList markers)
        {
            Logger.Log(this, "checkMarkers: " + markers.Count);
            for (int i = 0; i < markers.Count; i++)
            {
                ArrayList marker = (ArrayList)markers[i];
                if ((int)((ArrayList)marker[1])[0] == markerID)
                {
                    Logger.Log(this, "Correct marker: " + (Running && looking));
                    if (Calculate(marker))
                        looking = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Change direction towards the marker and return true iff we reached our destination.
        /// </summary>
        /// <param name="marker"></param>
        /// <returns></returns>
        private bool Calculate(ArrayList marker)
        {
            bool reached = false;
            float angle = ((float)((ArrayList)marker[0])[1]) / 4F;
            Call(() => Walk.Instance.StartWalking(speed, 0, Math.Max(-1, Math.Min(1, angle))));
            
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
