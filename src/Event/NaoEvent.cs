using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

namespace Naovigate.Event
{
    /// <summary>
    /// An abstract class implementing a simple NaoEvent interface.
    /// </summary>
    public abstract class NaoEvent : INaoEvent
    {
        /// <summary>
        /// Create a no-arguments-required event.
        /// The event's priority is medium.
        /// </summary>
        public NaoEvent()
        {
            Stream = GoalCommunicator.Instance.Stream;
            Priority = Priority.Low;
            ExecutionBehavior = ExecutionBehavior.Durative;
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
        /// Creates an empty NaoEvent instance with the specified execution behavior.
        /// </summary>
        /// <param name="e">A behavior. Either Durative or Instantaneous.</param>
        public NaoEvent(ExecutionBehavior e)
            : this()
        {
            ExecutionBehavior = e;
        }

        /// <summary>
        /// Creates an empty NaoEvent instance with the specified priority and execution behavior.
        /// </summary>
        /// <param name="p">A priority. Either Low, Medium or High.</param>
        /// <param name="e">A behavior. Either Durative or Instantaneous.</param>
        public NaoEvent(Priority p, ExecutionBehavior e)
            : this()
        {
            Priority = p;
            ExecutionBehavior = e;
        }

        /// <summary>
        /// The internal CommunicationStream of this event.
        /// </summary>
        public ICommunicationStream Stream
        {
            get;
            protected set;
        }

        /// <summary>
        /// This event's priority.
        /// </summary>
        public Priority Priority
        {
            get;
            protected set;
        }

        /// <summary>
        /// This event's execution trait.
        /// </summary>
        public ExecutionBehavior ExecutionBehavior
        {
            get;
            protected set;
        }

        /// <summary>
        /// This event's byte-code as defined by the GOAL-Nao API.
        /// </summary>
        public abstract EventCode EventCode
        {
            get;
        }

        /// <summary>
        /// True if this event's execution was aborted.
        /// </summary>
        public bool Aborted
        {
            get;
            private set;
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
