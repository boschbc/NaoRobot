using System;

namespace Naovigate.Navigation
{
    /// <summary>
    /// An entry in a route that can be easily converted to Nao commands. Contains a direction to walk in, a markerID to look for and the distance to that marker.
    /// </summary>
    public class RouteEntry
    {
        private Map.Direction dir;
        private int markerID;
        private float distance;

        public RouteEntry(Map.Direction dir, int markerID, float distance)
        {
            this.dir = dir;
            this.markerID = markerID;
            this.distance = distance;
        }

        /// <summary>
        /// The direction to walk to.
        /// </summary>
        /// <value>The direction.</value>
        public Map.Direction Direction
        {
            get { return this.dir; }
        }

        /// <summary>
        /// The marker ID to look for.
        /// </summary>
        /// <value>The marker ID.</value>
        public int MarkerID
        {
            get { return this.markerID; }
        }

        /// <summary>
        /// The distance towards the marker.
        /// </summary>
        /// <value>The distance.</value>
        public float Distance
        {
            get { return this.distance; }
        }
    }
}