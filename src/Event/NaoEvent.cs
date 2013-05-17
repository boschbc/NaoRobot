using System;
using Naovigate.Communication;

namespace Naovigate.Event
{
    /*
     * An abstract class implementing a simple NaoEvent interface.
     */
    public abstract class NaoEvent : INaoEvent
    {
        private Priority priority;
        protected CommunicationStream stream;

        /*
         * Creates an empty NaoEvent.
         */
        public NaoEvent()
        {
            stream = GoalCommunicator.Instance.Coms;
            priority = Priority.Medium;
        }

        /*
         * Creates an empty NaoEvent instance with the specified priority.
         * @param p - The priority this event will initially be set to.
         */
        public NaoEvent(Priority p) : this()
        {
            priority = p;
        }

        /*
         * The internal CommunicationStream of this event.
         */
        public CommunicationStream Stream
        {
            get{ return stream; }
            set{ stream = value; }
        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         */
        public Priority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public abstract void Fire();
        
        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public abstract void Abort();
    }
}
