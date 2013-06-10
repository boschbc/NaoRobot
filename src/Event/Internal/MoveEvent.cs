using System;
using System.Drawing;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event that makes the Nao move towards a given destination.
    /// </summary>
    public sealed class MoveEvent : NaoEvent
    {
        private PointF point;
        private float rotation;

        /// <summary>
        /// Default constructor will extract the event's parameters from a stream.
        /// </summary>
        public MoveEvent() 
        {
            Unpack();
        }

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="rotation">Rotation in radians, in interval [-pi, pi].</param>
        public MoveEvent(float x, float y, float rotation)
        {
            SetPoint(x, y, rotation);
        }

        /// <summary>
        /// Extract this event's parameters out of a stream.
        /// </summary>
        private void Unpack()
        {
            SetPoint(Stream.ReadInt(), Stream.ReadInt(), Stream.ReadInt());
        }

        /// <summary>
        /// Sets the moves destination.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="rotation">Rotation in radians, in interval [-pi, pi].</param>
        public void SetPoint(float x, float y, float rotation)
        {
            point = new PointF(x, y);
            this.rotation = rotation;
        }

        /// <summary>
        /// Begins this event's execution.
        /// </summary>
        public override void Fire()
        {
            Walk.Instance.WalkTo(point.X, point.Y, rotation);
        }

        /// <summary>
        /// Stop walking.
        /// </summary>
        public override void Abort()
        {
            base.Abort();
            Walk.Instance.StopMoving();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Move; }
        }
        /// <summary>
        /// Returns a human-readable string describing an instance of this class.
        /// </summary>
        /// <returns>A human readable string.</returns>
        public override string ToString()
        {
            return base.ToString() + String.Format("<point={0}, rotation={1}>", point, rotation);
        }
    }
}
