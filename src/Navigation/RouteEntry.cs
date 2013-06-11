using System;

namespace Naovigate.Navigation
{
    /// <summary>
    /// An entry in a route that can be easily converted to Nao commands.
    /// Contains a direction to walk in, a markerID to look for and the distance to that marker.
    /// </summary>
    public class RouteEntry : IEquatable<RouteEntry>
    {
        public RouteEntry(Direction dir, int markerID, int distance, int wantedDistance)
        {
            this.Direction = dir;
            this.MarkerID = markerID;
            this.Distance = distance;
            this.WantedDistance = wantedDistance;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Naovigate.Navigation.RouteEntry"/> is equal to the current <see cref="Naovigate.Navigation.RouteEntry"/>.
        /// </summary>
        /// <param name="other">The <see cref="Naovigate.Navigation.RouteEntry"/> to compare with the current <see cref="Naovigate.Navigation.RouteEntry"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Naovigate.Navigation.RouteEntry"/> is equal to the current
        /// <see cref="Naovigate.Navigation.RouteEntry"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(RouteEntry other)
        {
            return this.MarkerID == other.MarkerID && this.Direction == other.Direction
                && this.WantedDistance == other.WantedDistance;
        }

        /// <summary>
        /// The direction to walk to.
        /// </summary>
        /// <value>The direction.</value>
        public Direction Direction
        {
            get;
            set;
        }

        /// <summary>
        /// The marker ID to look for.
        /// </summary>
        /// <value>The marker ID.</value>
        public int MarkerID
        {
            get;
            set;
        }

        /// <summary>
        /// The distance towards the marker.
        /// </summary>
        /// <value>The distance.</value>
        public int Distance
        {
            get;
            set;
        }

        /// <summary>
        /// The wanted distance towards the marker.
        /// </summary>
        /// <value>The wanted distance.</value>
        public int WantedDistance
        {
            get;
            set;
        }

        public override string ToString()
        {
            return base.ToString() + " " + Direction + ", " + MarkerID + ", " + Distance + " " + WantedDistance;
        }
    }
}