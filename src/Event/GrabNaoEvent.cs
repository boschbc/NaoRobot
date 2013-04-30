using System;
using System.Collections.Generic;
using Naovigate.Communication;

namespace Naovigate.Event
{
    /**
    * A class representing the "grab" Nao-event.
    **/
    class GrabNaoEvent : NaoEvent
    {
        public GrabNaoEvent(CommunicationStream stream) : base(stream) { }

        public GrabNaoEvent() { }

        /**
         * Extracts parameters from a communication stream.
         **/
        private void Unpack(CommunicationStream stream)
        {
            //Extract parameters (if any)
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/ 
        public override void Fire()
        {
            //ControllerClass.Grab()
        }

        /**
         * Returns a human-readable string describing an instance of this class.
         **/
        public override string ToString()
        {
            return "GrabNaoEvent";
        }
    }
}
