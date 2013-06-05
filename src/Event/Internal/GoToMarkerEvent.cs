using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;
using Naovigate.Navigation;
using Naovigate.Util;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// Move to the marker MarkerID, if it is in range of the Nao.
    /// The Nao will stop Distance rooms away from the marker.
    /// </summary>
    public class GoToMarkerEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.GoTo;

        private int markerID;
        private int distance;

        private MarkerSearchThread worker;
        private bool aborted;

        /// <summary>
        /// Creates a new GoToMarkerEvent.
        /// </summary>
        public GoToMarkerEvent()
        {
            Unpack();
        }

        /// <summary>
        /// Creates a new GoToMarkerEvent using the given locations.
        /// </summary>
        /// <param name="locations">List of MarkerID's</param>
        public GoToMarkerEvent(int id, int distance)
        {
            this.markerID = id;
            this.distance = distance;
        }

        /// <summary>
        /// Extract the MarkerID and Distance parameters from the communication stream.
        /// </summary>
        private void Unpack()
        {
            markerID = stream.ReadInt();
            distance = stream.ReadInt();
        }

        /// <summary>
        /// Fire this event.
        /// </summary>
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code);
            try
            {
                if (!aborted)
                {
                    worker = Walk.Instance.WalkTowardsMarker(0, markerID, distance);
                    worker.WaitFor();
                }
            }
            catch(Exception e)
            {
                Logger.Log(this, e.Message);
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /// <summary>
        /// Abort this event's execution.
        /// </summary>
        public override void Abort()
        {
            aborted = true;
            try
            {
                if (worker != null)
                    worker.Abort();
            }
            catch
            {
                EventQueue.Goal.Post(new ErrorEvent());
            }
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.GoToMarker; }
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            return "GoToMarkerEvent("+markerID+", "+distance+")";
        }

    }
}
