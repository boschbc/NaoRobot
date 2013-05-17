﻿using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;

namespace Naovigate.Event.GoalToNao
{
    /*
     * @param MarkerID the id of a marker.
     * @param Distance the Nao will stop Distance rooms away from the marker.
     * Move to the marker MarkerID, if it is in range of the Nao.
     * The Nao will stop Distance rooms away from the marker.
     */
    public class GoToEvent : NaoEvent
    {
        private int theta;
        private int markerID;
        private int distance;

        /*
         * Default constructor.
         */
        public GoToEvent()
        {
            Unpack();
        }

        /*
         * Explicit constructor.
         */
        public GoToEvent(int theta, int markerID, int distance)
        {
            this.theta = theta;
            this.markerID = markerID;
            this.distance = distance;
        }

        /*
         * Extract the MarkerID and Distance parameters from a communication stream.
         */
        private void Unpack()
        {
            theta = stream.ReadInt();
            markerID = stream.ReadInt();
            distance = stream.ReadInt();
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(EventQueue.Instance.GetID(this)); ;
            try
            {
                Walk.Instance.WalkTowardsMarker(theta, markerID, distance);
            }
            catch
            {
                statusEvent = new FailureEvent(EventQueue.Instance.GetID(this));
            }
            EventQueue.Instance.Enqueue(statusEvent);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Abort()
        {
            try
            {
                Walk.Instance.Abort();
            }
            catch
            {
                EventQueue.Instance.Enqueue(new ErrorEvent());
            }
        }

        
    }
}
