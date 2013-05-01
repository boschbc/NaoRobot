using System;
using Naovigate.Communication;

namespace Naovigate.Event
{
    /**
    * A class representing the "look" Nao-event.
    **/
    public class LookNaoEvent : NaoEvent
    {
        private float degree;

        public LookNaoEvent(CommunicationStream stream) : base(stream) { }

        public LookNaoEvent(float degree)
        {
            SetDegree(degree);
        }

        /**
         * Extracts the degree parameter from a communication stream.
         **/
        private void Unpack(CommunicationStream stream)
        {
            degree = stream.ReadInt();
        }

        /**
         * Programmatically set the degree parameter of this event.
         **/
        public void SetDegree(float degree_)
        {
            degree = degree_;
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/ 
        public override void Fire()
        {
            //ControllerClass.look(degree)
        }

        /**
         * Returns a human-readable string describing an instance of this class.
         **/
        public override string ToString()
        {
            return String.Format("LookNaoEvent(degree={0})", degree);
        }
    }
}
