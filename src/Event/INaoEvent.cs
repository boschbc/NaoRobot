using System;

using Naovigate.Communication;
namespace Naovigate.Event
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
