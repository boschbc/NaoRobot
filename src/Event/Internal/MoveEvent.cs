using System;
using System.Windows;
using System.Drawing;
using Naovigate.Communication;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    /*
     * A class representing the "move" Nao-event.
     */
    class MoveEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Move;
        private PointF delta;
        private float rotation;

        /*
         * Default contructor.
         */
        public MoveEvent() 
        {
            Unpack();
        }

        public MoveEvent(float deltaX, float deltaY, float rotation)
        {
            SetDelta(deltaX, deltaY, rotation);
        }

        /*
         * Extracts the destination parameter from a communication stream.
         */
        private void Unpack()
        {
            SetDelta(stream.ReadInt(), stream.ReadInt(), stream.ReadInt());
        }

        /*
         * Programmatically set the move's destination.
         */
        public void SetDelta(float x, float y, float rotation)
        {
            delta = new PointF(x, y);
            this.rotation = rotation;
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */ 
        public override void Fire()
        {
            Walk.Instance.WalkTo(delta.X, delta.Y, rotation);
        }

        /// <summary>
        /// Stop walking.
        /// </summary>
        public override void Abort()
        {
            Walk.Instance.StopMove();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Move; }
        }

        /*
         * Returns a human-readable string describing an instance of this class.
         */
        public override string ToString()
        {
            return String.Format("MoveEvent(delta={0})", delta);
        }
    }
}
