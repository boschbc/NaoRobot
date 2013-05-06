using System;
using Naovigate.Communication;
namespace Naovigate.Event
{
    /**
     * An abstract class implementing a simple NaoEvent interface.
     **/
    public abstract class NaoEvent : INaoEvent
    {
        private Priority priority;

        /**
         * Creates an empty NaoEvent.
         **/
        public NaoEvent()
        {
            priority = Priority.Medium;
        }

        /**
         * Takes a communication stream, extracts parameters, and creates a new NaoEvent instance.
         * The NaoEvent's priority is set to medium.
         **/
        public NaoEvent(CommunicationStream stream)
        {
            Unpack(stream);
            priority = Priority.Medium;
        }

        /**
         * Takes a communication stream, extracts parameters, and creates a new NaoEvent instance.
         * @param p - The priority this event will initially be set to.
         **/
        public NaoEvent(CommunicationStream stream, Priority p)
        {
            Unpack(stream);
            priority = p;
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public Priority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public abstract void Fire();
        
        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public void Abort() { };

        /**
         * Takes a communication stream and extracts different parameters as required.
         * All subclasses should provide an implementation of this method.
         **/
        protected abstract void Unpack(CommunicationStream stream);
    }
}
