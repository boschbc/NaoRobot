using System;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;

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

        private PriorityQueue<INaoEvent> q;
        private Thread thread;
        private bool suspended;
        private bool running;

        private EventWaitHandle locker = new AutoResetEvent(false);
        
        /// <summary>
        /// Boolean saying if the event queue is handling an event.
        /// </summary>
        private bool inAction;

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
        /// True if the queue's main loop is currently running.
        /// </summary>
        public bool IsRunning
        {
            get { return running; }
        }

        /// <summary>
        /// Posts one or more events into the queue.
        /// Throws InvalidOpertionException if the queue was terminated prior to this method-invocation.
        /// </summary>
        /// <param name="events">One or more events.</param>
        public void Post(params INaoEvent[] events)
        {
            if (!IsRunning)
                throw new InvalidOperationException("Cannot post events to a terminated queue.");
            lock (q)
            {
                foreach (INaoEvent e in events)
                {
                    Logger.Log(this, "Posting event: " + e.ToString());
                    q.Enqueue(e, (int)e.Priority);
                }
            }
            locker.Set();
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
                    FireEvent();
                }
                locker.WaitOne();
            }
            Logger.Log(this, "Exiting main loop.");
        }

        /// <summary>
        /// The next event in the queue.
        /// </summary>
        INaoEvent NextEvent
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
        /// Fires an event.
        /// </summary>
        private void FireEvent()
        {
            inAction = true;
            INaoEvent e = NextEvent;
            if (e != null)
            {
                // there was an event available, fire it
                Logger.Log(this, "Firing " + e + ".\n" + EventsQueuedCount() + " events pending.");
                e.Fire();
                Logger.Log(this, "Event " + e + " finished firing.");
            }
            inAction = false;
        }

        /// <summary>
        /// The amount of pending events.
        /// </summary>
        /// <returns>The amount of pending events.</returns>
        public int EventsQueuedCount()
        {
            return q.Size();
        }

        /// <summary>
        /// Returns true if there are no pending events.
        /// </summary>
        /// <returns>A boolean.</returns>
        public bool IsEmpty()
        {
            return EventsQueuedCount() == 0 && !inAction;
        }
        
        /// <summary>
        /// Blocks the thread until the next time the queue becomes empty or suspended.
        /// </summary>
        public void WaitFor()
        {
            while (!suspended && !q.IsEmpty())
                Thread.Sleep(100);
        }

        /// <summary>
        /// Clears all queued events and stops accepting new events to be queued.
        /// Exits the main thread.
        /// </summary>
        public void Abort()
        {
            q.Clear();
            running = false;
            locker.Set();
            Logger.Log(this, "Shutting down...");
        }
    }
}
