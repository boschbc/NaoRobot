using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

namespace Naovigate.Event
{
    /// <summary>
    /// The EventQueue collects events for activation, and fires them one at a time.
    /// To achieve interruptable events by high-prioritized stop events, events should
    /// be handled quickly
    ///      e.g. an action that makes the robot stand up and walk a meter, should
    ///      create multiple events for standing up and walking, preferably splitting
    ///     the walk in multiple events aswell.
    /// </summary>
    public class EventQueue
    {
        private static EventQueue naoInstance;
        private static EventQueue goalInstance;

        protected event Action<INaoEvent> EventPosted;
        protected event Action<INaoEvent> EventFired;
        
        private PriorityQueue<INaoEvent> q;
        private Thread thread;
        private bool suspended;
        private bool running;

        private EventWaitHandle locker = new AutoResetEvent(false);

        /// <summary>
        /// The EventQueue instance for incoming events.
        /// </summary>
        public static EventQueue Nao
        {
            get
            {
                if (naoInstance == null)
                {
                    naoInstance = new EventQueue();
                }
                return naoInstance;
            }
        }
        private INaoEvent current;

        /// <summary>
        /// The EventQueue instance for outgoing events.
        /// </summary>
        public static EventQueue Goal
        {
            get
            {
                if (goalInstance == null)
                {
                    goalInstance = new EventQueue();
                }
                return goalInstance;
            }
        }

        /// <summary>
        /// The current event being fired.
        /// Equals null if there is no event firing.
        /// </summary>
        public INaoEvent CurrentlyFiring
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new EventQueue instance and starts the main thread.
        /// </summary>
        public EventQueue()
        {
            q = new PriorityQueue<INaoEvent>(4);
            running = true;
            thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// Subscribes a given method to this goal communicator.
        /// The method will be invoked each time an event was posted.
        /// </summary>
        /// <param name="handler">The handler which will be called when an event is posted.</param>
        public void SubscribePost(Action<INaoEvent> handler)
        {
            EventPosted += handler;
        }

        /// <summary>
        /// Subscribes a given method to this goal communicator.
        /// The method will be invoked each time an event was posted.
        /// </summary>
        /// <param name="handler">The handler which will be called when an event is posted.</param>
        public void SubscribeFire(Action<INaoEvent> handler)
        {
            EventFired += handler;
        }

        /// <summary>
        /// Unsubscribes all handlers from both the Post and Fire events.
        /// </summary>
        public void UnsubscribeAll()
        {
            EventPosted = null;
            EventFired = null;
        }

        /// <summary>
        /// True if the queue's main loop is currently running.
        /// </summary>
        public bool IsRunning
        {
            get { return running; }
        }

        /// <summary>
        /// Posts an event to the queue.
        /// </summary>
        /// <param name="events">A NaoEvent.</param>
        /// <exception cref="InvalidOperationException">The queue was terminated prior to this method-invocation.</exception>
        public void Post(INaoEvent e)
        {
            if (!IsRunning)
                throw new InvalidOperationException("Cannot post events to a terminated queue.");
            if (e.ExecutionBehavior == ExecutionBehavior.Instantaneous)
            {
                FireEvent(e);
                return;
            }
            lock (q)
            {
                if (EventPosted != null)
                    EventPosted(e);
                Logger.Log(this, "Posting event: " + e.ToString());
                q.Enqueue(e, (int)e.Priority);  
            }
            locker.Set();
        }

        /// <summary>
        /// Posts one or more events into the queue.
        /// </summary>
        /// <param name="events">One or more events.</param>
        public void Post(params INaoEvent[] events)
        {
            lock (q)
            {
                foreach (INaoEvent e in events)
                    Post(e);
            }
        }

        /// <summary>
        /// Suspends the firing of events. events may still be added to the queue whlie suspended.
        /// </summary>
        public void Suspend()
        {
            Logger.Log(this, "Suspended.");
            suspended = true;
        }

        /// <summary>
        /// Continue firing events.
        /// If the EventQueue was not suspended, has no effect.
        /// </summary>
        public void Resume()
        {
            if (!suspended)
                return;
            Logger.Log(this, "Resumed.");
            suspended = false;
            locker.Set();
        }

        /// <summary>
        /// The queue's main loop. Iterates through incoming events and fires them in sequence.
        /// </summary>
        private void Run()
        {
            Logger.Log(this, "Entering main loop...");
            running = true;
            while (IsRunning)
            {
                while (!IsEmpty() && !suspended)
                {
                    FireNextEvent();
                }
                locker.WaitOne();
            }
            Logger.Log(this, "Exiting main loop.");
        }

        /// <summary>
        /// Returns the next event in the queue while removing it from the queue.
        /// </summary>
        /// <returns>The next event in queue, or null if the queue is empty.</returns>
        protected INaoEvent NextEvent
        {
            get
            {
                // lock queue, we dont want concurrent modifications
                lock (q)
                {
                    if (!q.IsEmpty())
                    {
                        return q.Dequeue();
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the next event in the queue while not removing it from the queue.
        /// </summary>
        /// <returns>The next event in queue, or null if the queue is empty.</returns>
        public INaoEvent Peek()
        {
            lock (q)
            {
                if (!q.IsEmpty())
                    return q.Peek();
            }
            return null;
        }

        /// <summary>
        /// Fires an event.
        /// </summary>
        private void FireNextEvent()
        {
            current = NextEvent;
            if (current != null)
            {
                FireEvent(current);
            }
        }

        private void FireEvent(INaoEvent e)
        {
            Logger.Log(this, "Firing: " + e);
            if (e.ExecutionBehavior == ExecutionBehavior.Durative)
            {
                CurrentlyFiring = e;
                e.Fire();
                CurrentlyFiring = null;
            }
            else
                e.Fire();
            if (EventFired != null)
                EventFired(e);
            Logger.Log(this, "Event " + e + " finished firing.");
        }

        public INaoEvent Current
        {
            get
            {
                return current;
            }
        }

        /// <summary>
        /// The amount of pending events.
        /// </summary>
        /// <returns>The amount of pending events.</returns>
        public int EventsQueuedCount()
        {
            lock (q)
                return q.Size();
        }

        /// <summary>
        /// Returns true if there are no pending events.
        /// </summary>
        /// <returns>A boolean.</returns>
        public bool IsEmpty()
        {
            return EventsQueuedCount() == 0 && !(CurrentlyFiring == null);
        }
        
        /// <summary>
        /// Blocks the thread until the next time the queue becomes empty or suspended.
        /// </summary>
        public void WaitFor()
        {
            while (!suspended && !IsEmpty())
                Thread.Sleep(100);
        }
        
        /// <summary>
        /// Clears all queued events. Any event that is currently being executed will not be interrupted.
        /// </summary>
        public void Clear()
        {
            lock (q)
                q.Clear();
        }

        public List<INaoEvent> ClearAndGet()
        {
            List<INaoEvent> events = new List<INaoEvent>();
            while (q.IsEmpty())
            {
                INaoEvent e = NextEvent;
                if(e != null) events.Add(e);
            }
            return events;
        }

        /// <summary>
        /// Clears all queued events and stops accepting new events to be queued.
        /// Exits the main thread.
        /// </summary>
        public void Abort()
        {
            Clear();
            running = false;
            locker.Set();
            Logger.Log(this, "Shutting down...");
        }

        /// <summary>
        /// Returns a string representation of this queue.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            return q.ToString();
        }
    }
}
