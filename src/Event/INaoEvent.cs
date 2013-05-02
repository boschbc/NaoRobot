using System;

using Naovigate.Communication;
namespace Naovigate.Event
{
    /**
     * A simple interface for Nao-events.
     **/
    public interface INaoEvent
    {
        /**
         * The event's priority property.
         **/
        Priority Priority
        {
            get;
            set;
        }

        /**
         * Fires the event.
         **/
        void Fire();
    }
}
