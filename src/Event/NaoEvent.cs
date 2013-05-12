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

        /*
         * Creates an empty NaoEvent.
         */
        public NaoEvent()
        {
            priority = Priority.Medium;
        }

        /*
         * Creates an empty NaoEvent instance with the specified priority.
         * @param p - The priority this event will initially be set to.
         */
        public NaoEvent(Priority p)
        {
            priority = p;
        }

        /*
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
