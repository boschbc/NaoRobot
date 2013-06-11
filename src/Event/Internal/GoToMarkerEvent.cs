using System;

using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// Move to the marker MarkerID, if it is in range of the Nao.
    /// The Nao will stop Distance rooms away from the marker.
    /// </summary>
    public sealed class GoToMarkerEvent : NaoEvent
    {
        private int markerID;
        private int distance;

        private MarkerSearchWorker worker;
        
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
            markerID = Stream.ReadInt();
            distance = Stream.ReadInt();
        }

        /// <summary>
        /// Fire this event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                if (!Aborted)
                {
                    Logger.Log("GoToMarker: start");
                    worker = Walk.Instance.WalkTowardsMarker(0, markerID, distance);

                    Logger.Log("Waitfor:"+worker.Running);
                    worker.WaitFor();
                }
            }
            catch(Exception e)
            {
                Logger.Log(this, e.Message);
            }
        }

        /// <summary>
        /// Abort this event's execution.
        /// </summary>
        public override void Abort()
        {
            base.Abort();
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
            return base.ToString() + String.Format("<MarkerID: {0}, Distance: {1}>", markerID, distance);
        }

    }
}
