﻿using System;
using System.Threading;
using System.Collections;
using Naovigate.Util;
using Naovigate.Movement;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    public class MarkerSearchThread : ActionExecutor
    {
        private bool found;
        private int markerID;
        private double dist;

        public MarkerSearchThread(int markerID, double dist)
        {
            found = false;
            this.markerID = markerID;
            this.dist = dist;
        }

        public override void Run()
        {
            LookForMarker();
        }

        public void LookForMarker()
        {
            MarkerRecogniser rec = MarkerRecogniser.GetInstance();
            ArrayList markers;

            while (running && !found)
            {
                ArrayList data = rec.GetMarkerData();
                markers = data.Count == 0 ? data : (ArrayList)data[1];
                for (int i = 0; running && !found && i < markers.Count; i++)
                {
                    ArrayList marker = (ArrayList)markers[i];
                    if (running && (int)((ArrayList)marker[1])[0] == markerID)
                    {
                        float angle = ((float)((ArrayList)marker[0])[1]) / 4F;
                        Walk.Instance.StartWalking(0.5F, 0, Math.Max(-1, Math.Min(1, angle)));

                        float sizeY = ((float)((ArrayList)marker[0])[4]);

                        if (running && MarkerRecogniser.estimateDistance(sizeY) <= dist)
                        {
                            Walk.Instance.StopMove();
                            found = true;
                        }
                    }
                }
                Thread.Sleep(250);
            }
            Console.WriteLine("Exit LookForMarker");
        }
    }
}