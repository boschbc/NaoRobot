using System;

namespace Naovigate.Communication
{
    /**
     * A simple interface for Nao-events.
     **/
    interface INaoEvent
    {
        /**
         * Set the event's priority.
         **/
        void SetPriority(Priority p);
        
        /**
         * Returns the event's priority.
         **/
        Priority GetPriority(Priority p);

        /**
         * Fires the event.
         **/
        void Fire();
    }
}
