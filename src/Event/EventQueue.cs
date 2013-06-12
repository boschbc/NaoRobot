using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Naovigate.Util;

namespace Naovigate.Event
{
    /// <summary>
    /// The EventQueue collects events for activation, and fires them one at a time.
    /// To achieve interruptable events by high-prioritized stop events, events should
    /// be handled quickly
    ///      e.g. an action that makes the robot stand up and walk a meter, should
    ///      create multiple events for standing up and walking, preferably splitting
    ///      the walk in multiple events aswell.
    /// </summary>
    public sealed class EventQueue
    {
        public event Action<INaoEvent> EventFiring;

        private static EventQueue naoInstance;
        private static EventQueue goalInstance;

        private PriorityQueue<INaoEvent> q;
        private Thread thread;
        private bool suspended;
        private bool inAction;
        private Stopwatch eventTimer = new Stopwatch();
     

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
                    naoInstance = new EventQueue("NaoQueue");
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
                    goalInstance = new EventQueue("GoalQueue");
                }
                return goalInstance;
            }
        }

        /// <summary>
        /// Creates a new EventQueue instance and starts the main thread.
        /// </summary>
        public EventQueue()
        {
            q = new PriorityQueue<INaoEvent>(4);
            thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// Creates a new EventQueue instance and starts the main thread.
        /// </summary>
        /// <param name="name">The name to give to the main thread.</param>
        public EventQueue(string name) : this()
        {
            thread.Name = name;
        }

        /// <summary>
        /// True if the queue's main loop is currently running.
        /// </summary>
        public bool Running
        {
            get;
            private set;
        }

        /// <summary>
        /// True if the queue's main loop is currently suspended.
        /// </summary>
        public bool Suspended
        {
            get { return suspended; }
            set
            {
                if (!value && suspended)
                {
                    Logger.Log(this, "Resumed.");
                    locker.Set();
                }
                else
                    Logger.Log(this, "Suspended.");
                suspended = value;    
            }
        }

        /// <summary>
        /// The current event being fired.
        /// </summary>
        public INaoEvent Current
        {
            get;
            private set;
        }


        /// <summary>
        /// Clears all subscribers of the 'EventFiring' event.
        /// </summary>
        public void ClearSubscribers()
        {
            EventFiring = null;
        }

        /// <summary>
        /// Posts an event to the queue.
        /// </summary>
        /// <param name="events">A NaoEvent.</param>
        /// <exception cref="InvalidOperationException">The queue was terminated prior to this method-invocation.</exception>
        public void Post(INaoEvent e)
        {
            if (e.ExecutionBehavior == ExecutionBehavior.Instantaneous)
            {
                FireEvent(e);
                return;
            }
            lock (q)
            {
                Logger.Log(this, "Posting event: " + e.ToString());
                q.Enqueue(e, (int)e.Priority);  
            }
            locker.Set();
            // log nao events pending
            if (this == Nao)
            {
                Logger.Log(this, ToString());
            }
        }

        /// <summary>
        /// Posts one or more events into the queue.
        /// </summary>
        /// <param name="events">One or more events.</param>
        public void Post(params INaoEvent[] events)
        {
            foreach (INaoEvent e in events)
                Post(e);
        }

        /// <summary>
        /// The queue's main loop. Iterates through incoming events and fires them in sequence.
        /// </summary>
        private void Run()
        {
            Logger.Log(this, "Entering main loop...");
            Running = true;
            while (Running)
            {
                while (!IsEmpty() && !suspended)
                {
                    inAction = true;
                    FireNextEvent();
                    inAction = false;
                }
                locker.WaitOne();
            }
            Logger.Log(this, "Exiting main loop.");
        }

        /// <summary>
        /// Returns the next event in the queue while removing it from the queue.
        /// </summary>
        /// <returns>The next event in queue, or null if the queue is empty.</returns>
        private INaoEvent NextEvent
        {
            get
            {
                lock (q)
                {
                    if (!q.IsEmpty())
                        return q.Dequeue();
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
        /// Fires the next event in queue.
        /// </summary>
        private void FireNextEvent()
        {
            Current = NextEvent;
            if (Current != null)
                FireEvent(Current);
            Current = null;
        }

        /// <summary>
        /// Fires given event.
        /// </summary>
        /// <param name="e">An event.</param>
        private void FireEvent(INaoEvent e)
        {
            Logger.Log(this, "Firing: " + e);
            if (EventFiring != null)
                EventFiring(e);
            e.Fire();
            eventTimer.Restart();
            Logger.Log(this, "Event " + e + " finished firing.");
        }

        
        /// <summary>
        /// The amount of pending events.
        /// </summary>
        /// <returns>The amount of pending events.</returns>
        public int Size()
        {
            lock (q)
                return q.Size();
        }

        /// <summary>
        /// Returns true if there are no pending events and no event is currently being fired.
        /// </summary>
        /// <returns>A boolean.</returns>
        public bool IsEmpty()
        {
            return Size() == 0 && !inAction;
        }

        /// <summary>
        /// Clears all queued events. Any event that is currently being executed will not be interrupted.
        /// </summary>
        public void Clear()
        {
            lock (q)
                q.Clear();
        }

        /// <summary>
        /// Blocks the thread until the next time the queue becomes empty or suspended.
        /// </summary>
        public void WaitFor()
        {
            Logger.Log(this, "waitfor");
            while (!suspended && !IsEmpty())
                Thread.Sleep(100);
            Logger.Log(this, "wait end");
        }

        /// <summary>
        /// Clears the queue and returns all events that were removed.
        /// </summary>
        /// <returns>List of events.</returns>
        public List<INaoEvent> ClearAndGet()
        {
            List<INaoEvent> events = new List<INaoEvent>();
            while (!q.IsEmpty())
            {
                INaoEvent e = NextEvent;
                if(e != null)
                    events.Add(e);
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
            Running = false;
            Logger.Log(this, "Shutting down...");
        }

        /// <summary>
        /// Returns a string representation of this queue.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            lock (q)
            {
                return q.ToString();
            }
        }
    }
}
