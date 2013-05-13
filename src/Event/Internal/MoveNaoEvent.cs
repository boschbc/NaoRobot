﻿using System;
using System.Windows;
using System.Drawing;
using Naovigate.Communication;

using Naovigate.Movement;
using Naovigate.Event.GoalToNao;

namespace Naovigate.Event
{
    /*
     * A class representing the "move" Nao-event.
     */
    public class MoveNaoEvent : NaoEvent
    {
       
        private PointF delta;

        /*
         * Default contructor.
         */
        public MoveNaoEvent() 
        {
            Unpack();
        }

        public MoveNaoEvent(float deltaX, float deltaY)
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
            return String.Format("MoveNaoEvent(delta={0})", delta);
        }
    }
}