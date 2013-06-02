﻿using System;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.Event
{
    /// <summary>
    /// An abstract class implementing a simple NaoEvent interface.
    /// </summary>
    public abstract class NaoEvent : INaoEvent
    {
        public static readonly EventCode code;

        private Priority priority;
        protected ICommunicationStream stream;
        private bool aborted;


        /// <summary>
        /// Create a no-arguments-required event.
        /// The event's priority is medium.
        /// </summary>
        public NaoEvent()
        {
            Stream = GoalCommunicator.Instance.Stream;
            Priority = Priority.Medium;
        }

        /// <summary>
        /// Creates an empty NaoEvent instance with the specified priority.
        /// </summary>
        /// <param name="p">A priority. Either Low, Medium or High.</param>
        public NaoEvent(Priority p) : this()
        {
            Priority = p;
        }

        /// <summary>
        /// The internal CommunicationStream of this event.
        /// </summary>
        public ICommunicationStream Stream
        {
            get { return stream; }
            protected set { stream = value; }
        }

        /// <summary>
        /// This event's priority.
        /// </summary>
        public Priority Priority
        {
            get { return priority; }
            protected set { priority = value; }
        }

        /// <summary>
        /// True if this event's execution was aborted.
        /// </summary>
        public bool Aborted
        {
            get { return aborted; }
            private set { aborted = value; }
        }
        /// <summary>
        /// Fires the event.
        /// </summary>
        public abstract void Fire();

        /// <summary>
        /// Blocks the current thread until this event's execution is completed.
        /// Has no effect if the event has not been fired.
        /// </summary>
        public virtual void WaitFor() { }

        /// <summary>
        /// Aborts execution of this event.
        /// </summary>
        public virtual void Abort()
        {
            Logger.Log(this, "Aborted.");
            Aborted = true;
        }

        /// <summary>
        /// Returns a human-readable representation of the event.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}
