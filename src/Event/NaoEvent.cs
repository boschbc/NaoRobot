using System;
using Naovigate.Communication;
namespace Naovigate.Event
{
    /**
     * An abstract class implementing a simple NaoEvent interface.
     **/
    abstract class NaoEvent : INaoEvent
    {
        private Priority priority;

        /**
         * Creates an empty NaoEvent.
         **/
        public NaoEvent()
        {
            SetPriority(Priority.Medium);
        }

        /**
         * Takes a communication stream, extracts parameters, and creates a new NaoEvent instance.
         * The NaoEvent's priority is set to medium.
         **/
        public NaoEvent(CommunicationStream stream)
        {
            Unpack(stream);
            SetPriority(Priority.Medium);
        }

        /**
         * Takes a communication stream, extracts parameters, and creates a new NaoEvent instance.
         * @param p - The priority this event will initially be set to.
         **/
        public NaoEvent(CommunicationStream stream, Priority p)
        {
            Unpack(stream);
            SetPriority(p);
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public void SetPriority(Priority p)
        {
            priority = p;
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public Priority GetPriority()
        {
            return priority;
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public abstract void Fire();
        
        /**
         * Takes a communication stream and extracts different parameters as required.
         * All subclasses should provide an implementation of this method.
         **/
        private void Unpack(CommunicationStream stream)
        {
            throw new NotImplementedException("Method Unpack() is not implemented.");
        }
    }
}
