using System;
using System.Windows;
using System.Drawing;

using Naovigate.Movement;

namespace Naovigate.Communication
{
    /**
     * A class representing the "move" Nao-event.
     **/
    class MoveNaoEvent : NaoEvent
    {
        private Point destination;

        public MoveNaoEvent(CommunicationStream stream) : base(stream) { }

        /**
         * Extracts the destination parameter from a communication stream.
         **/
        private void Unpack(CommunicationStream stream)
        {
            destination = new Point(stream.ReadInt(), stream.ReadInt());
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/ 
        public override void Fire()
        {
            Walk.WalkTo(destination.X, destination.Y, 0.0f);
        }
    }
}
