using System;
using Naovigate.Communication;

namespace Naovigate.Event
{
    /*
     * Enumeration describing the different priority levels of Nao-events.
     */
    public enum Priority
    {
        Low,
        Medium,
        High
    };

    /*
     * Enumeration describing the different event-codes as declared in the API.
     */
    public enum EventCode
    {
        //Nao to Goal
        Error = 0x80,
        Success = 0x81,
        Failure = 0x82,
        Location = 0x89,
        DistanceTo = 0x8A,
        AtObject = 0x8B,
        See = 0x8C,
        Holding = 0x8D,
        Agent = 0x8E,
        State = 0x8F,
        DataSending = 0x83,
        MapOverview = 0x84,

        //Goal to Nao
        Exit = 0x00,
        GoTo = 0x01,
        Pickup = 0x02,
        PutDown = 0x03,
        Halt = 0x04,

        //Internal
        Move = 0x00,
        Colliding = 0x01,
        StandUp = 0x02,
        SitDown = 0x03
    }

    /*
     * A simple interface for Nao-events.
     */
    public interface INaoEvent
    {
        /*
         * The event's priority property.
         */
        Priority Priority
        {
            get;
            set;
        }

        /*
         * Fires the event.
         */
        void Fire();

        /*
         * Aborts this event's operation, if it is durative.
         */
        void Abort();
    }
}
