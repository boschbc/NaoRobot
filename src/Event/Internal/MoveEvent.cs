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

        /*
         * Default contructor.
         */
        public MoveEvent() 
        {
            Unpack();
        }

        public MoveEvent(float deltaX, float deltaY)
        {
            SetDelta(deltaX, deltaY);
        }

        /*
         * Extracts the destination parameter from a communication stream.
         */
        private void Unpack()
        {
            SetDelta(stream.ReadInt(), stream.ReadInt());
        }

        /*
         * Programmatically set the move's destination.
         */
        public void SetDelta(float x, float y)
        {
            delta = new PointF(x, y);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */ 
        public override void Fire()
        {
            Walk.Instance.WalkTo(delta.X, delta.Y, 0.0f);
        }

        /*
         * unimplemented
         */
        public override void Abort()
        {
            throw new NotImplementedException();
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
