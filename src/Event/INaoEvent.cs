using System;
using Naovigate.Communication;

namespace Naovigate.Event
{
    public enum Priority
    {
        Low,
        Medium,
        High
    };

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
