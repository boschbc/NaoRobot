﻿
namespace Naovigate.Event
{
    /// <summary>
    /// The two possible trait for the execution of an event.
    /// </summary>
    public enum ExecutionBehavior
    {
        Durative,
        Instantaneous
    }

    /// <summary>
    /// Enumeration describing the different priority levels of Nao-events.
    /// </summary>
    public enum Priority
    {
        Low,
        Medium,
        High
    };

    /// <summary>
    /// Enumeration describing the different event-codes as declared in the API.
    /// </summary>
    public enum EventCode
    {
        //Nao to Goal
        Error = 0x80,
        Success = 0x81,
        Failure = 0x82,
        Location = 0x89,
        //DistanceTo = 0x8A,
        //AtObject = 0x8B,
        See = 0x8A,
        Holding = 0x8D,
        Dropped = 0x8C,
        Agent = 0x8E,
        State = 0x8F,
        //DataSending = 0x83,
        //MapOverview = 0x84,
        //LookForObject = 0x85,
        

        //Goal to Nao
        Exit = 0x00,
        GoTo = 0x01,
        Pickup = 0x02,
        PutDown = 0x03,
        Halt = 0x04,
        Say = 0x05,

        //Internal
        Move = 0x40,
        Colliding = 0x41,
        StandUp = 0x42,
        SitDown = 0x43,
        ShutDown = 0x44,
        Crouch = 0x45,
        GoToMarker = 0x46,
        Grab = 0x47,
        TurnRelative = 0x48,
        TurnAbsolute = 0x49,
        LookForObject = 0x50,
        Test = 0xFD,
        Report = 0xFE,
        KeepAlive = 0xFF,
    }

    /// <summary>
    /// A simple interface for NaoEvents.
    /// </summary>
    public interface INaoEvent
    {
        /// <summary>
        /// The event's execution-trait property.
        /// </summary>
        ExecutionBehavior ExecutionBehavior
        {
            get;
        }

        /// <summary>
        /// The event's priority property.
        /// </summary>
        Priority Priority
        {
            get;
        }

        /// <summary>
        /// The event's byte-code as defined in the GOAL-Nao API.
        /// </summary>
        EventCode EventCode
        {
            get;
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        void Fire();

        /// <summary>
        /// Aborts this event's operation, if it is durative.
        /// </summary>
        void Abort();
    }
}
