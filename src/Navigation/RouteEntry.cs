using System;

namespace Naovigate.Navigation
{
    /// <summary>
    /// An entry in a route that can be easily converted to Nao commands. Contains a direction to walk in, a markerID to look for and the distance to that marker.
    /// </summary>
    public class RouteEntry : IEquatable<RouteEntry>
    {
        private Map.Direction dir;
        private int markerID;
        private int distance;
        private int wantedDistance;

        public RouteEntry(Map.Direction dir, int markerID, int distance, int wantedDistance)
        {
            this.dir = dir;
            this.markerID = markerID;
            this.distance = distance;
            this.wantedDistance = wantedDistance;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Naovigate.Navigation.RouteEntry"/> is equal to the current <see cref="Naovigate.Navigation.RouteEntry"/>.
        /// </summary>
        /// <param name="other">The <see cref="Naovigate.Navigation.RouteEntry"/> to compare with the current <see cref="Naovigate.Navigation.RouteEntry"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Naovigate.Navigation.RouteEntry"/> is equal to the current
        /// <see cref="Naovigate.Navigation.RouteEntry"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(RouteEntry other)
        {
            return this.markerID == other.markerID && this.dir == other.dir
                && this.wantedDistance == other.wantedDistance;
        }

        /// <summary>
        /// The direction to walk to.
        /// </summary>
        /// <value>The direction.</value>
        public Map.Direction Direction
        {
            get { return this.dir; }
            set { this.dir = value; }
        }

        /// <summary>
        /// The marker ID to look for.
        /// </summary>
        /// <value>The marker ID.</value>
        public int MarkerID
        {
            get { return this.markerID; }
            set { this.markerID = value; }
        }

        /// <summary>
        /// The distance towards the marker.
        /// </summary>
        /// <value>The distance.</value>
        public int Distance
        {
            get { return this.distance; }
            set { this.distance = value; }
        }

        /// <summary>
        /// The wanted distance towards the marker.
        /// </summary>
        /// <value>The wanted distance.</value>
        public int WantedDistance
        {
            get { return this.wantedDistance; }
            set { this.wantedDistance = value; }
        }
    }
}