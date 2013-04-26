﻿using System;
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
        private PointF delta;

        public MoveNaoEvent(CommunicationStream stream) : base(stream) { }

        public MoveNaoEvent(float deltaX, float deltaY)
        {
            SetDelta(deltaX, deltaY);
        }

        /**
         * Extracts the destination parameter from a communication stream.
         **/
        private void Unpack(CommunicationStream stream)
        {
            SetDelta(stream.ReadInt(), stream.ReadInt());
        }

        /**
         * Programmatically set the move's velocity.
         **/
        public void SetDelta(float x, float y)
        {
            delta = new PointF(x, y);
        }
        /**
         * See the INaoEvent class docs for documentation of this method.
         **/ 
        public override void Fire()
        {
            //Walk.WalkTo(delta.X, delta.Y, 0.0f);
        }
    }
}