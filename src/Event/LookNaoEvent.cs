using System;
using Naovigate.Communication;

namespace Naovigate.Event
{
    /**
    * A class representing the "look" Nao-event.
    **/
    class LookNaoEvent : NaoEvent
    {
        private static int DegreeIndex = 0;

        private int degree;

        public LookNaoEvent(CommunicationStream stream) : base(stream) { }

        /**
         * Extracts the degree parameter from a communication stream.
         **/
        private void Unpack(CommunicationStream stream)
        {
            degree = stream.ReadInt();
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/ 
        public override void Fire()
        {
            //ControllerClass.look(degree)
        }
    }
}
