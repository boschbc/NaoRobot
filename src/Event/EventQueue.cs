using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Util;
using System.Threading;
using System.ComponentModel;
namespace Naovigate.Event
{
    /*
     * The EventQueue collects events for activation, and fires them one at a time.
     * To achieve interruptable events by high-prioritized stop events, events should
     * be handled quickly
     *      e.g. an action that makes the robot stand up and walk a meter, should
     *      create multiple events for standing up and walking, preferably splitting
     *      the walk in multiple events aswell.
     */
    public class EventQueue
    {

        private static EventQueue instance;
        private Object locker;

        /*
         * internal queue to store the events
         */
        private PriorityQueue<INaoEvent> q;
        private Thread thread;

        /*
         * boolean saying if the event queue is handling an event
         */
        private bool inAction;

        public EventQueue()
        {
            q = new PriorityQueue<INaoEvent>(3);
            thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();
        }

        public void Post(params INaoEvent[] events)
        {
            Enqueue(events);
        }

        public void Enqueue(params INaoEvent[] events)
        {
            // todo: synchronize
            Monitor.Pulse(locker);
            lock (q)
            {
                foreach(INaoEvent e in events)
                    q.Enqueue(e, (int) e.GetPriority());
                //Monitor.Pulse(this);
            }
        }

        /**
        * return the EventQueue instance
        */
        public static EventQueue Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EventQueue();
                }
                return instance;
            }
        }

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(100);
                lock (locker)
                {
                    Monitor.Wait(locker);
                }
                inAction = true;
                INaoEvent e = null;

                // lock queue, we dont want concurrent modifications
                lock (q)
                {
                    if (!q.IsEmpty())
                    {
                        e = q.Dequeue();
                    }
                }
                // there was an event available, fire it
                if (e != null)
                {
                    Console.WriteLine("Firing " + e);
                    e.Fire();
                }
                inAction = false;
            }
        }

        public int EventsQueuedCount()
        {
            return q.Size();
        }

        public bool IsEmpty()
        {
            return q.IsEmpty() && !inAction;
        }
    }
}
