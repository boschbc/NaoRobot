using System;
using System.Windows;
using System.Drawing;
using Naovigate.Communication;

using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// An event that makes the Nao move towards a given destination.
    /// </summary>
    class MoveEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Move;
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
            SetPoint(stream.ReadInt(), stream.ReadInt(), stream.ReadInt());
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
            Walk.Instance.StopMoving();
        }

        /// <summary>
        /// Returns a human-readable string describing an instance of this class.
        /// </summary>
        /// <returns>A human readable string.</returns>
        public override string ToString()
        {
            return String.Format("MoveEvent(point={0}, rotation={1})", point, rotation);
        }
    }
}
