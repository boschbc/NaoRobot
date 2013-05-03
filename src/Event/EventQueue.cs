﻿using System;
using System.Collections.Generic;
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
        private static EventQueue goal;

        /*
         * internal queue to store the events
         */
        private PriorityQueue<INaoEvent> q;
        private Thread thread;

        private EventWaitHandle locker = new AutoResetEvent(false);

        /*
         * boolean saying if the event queue is handling an event
         */
        private bool inAction;

        public EventQueue()
        {
            q = new PriorityQueue<INaoEvent>(4);
            thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();
        }

        public void Post(params INaoEvent[] events)
        {
            Console.WriteLine("Posting Event(s)");
            Enqueue(events);
            Console.WriteLine("Posted Event(s)");
        }

        public void Enqueue(params INaoEvent[] events)
        {
            lock (q)
            {
                foreach (INaoEvent e in events)
                {
                    Console.WriteLine("Enqueue "+e);
                    q.Enqueue(e, (int)e.Priority);
                    Console.WriteLine(e+" Added");
                }
            }
            locker.Set();
        }

        /**
        * return the EventQueue instance for nao events
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

       /**
       * return the EventQueue instance for sending 
       */
        public static EventQueue Goal
        {
            get
            {
                if (goal == null)
                {
                    goal = new EventQueue();
                }
                return goal;
            }
        }

        private void Run()
        {
            while (true)
            {
                while (!IsEmpty())
                {
                    FireEvent();
                }
                locker.WaitOne();
            }
        }

        private void FireEvent()
        {
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
            if (e != null)
            {
                // there was an event available, fire it
                Console.WriteLine("Firing " + e + "\n" + EventsQueuedCount()+" Events left.");
                e.Fire();
            }
            inAction = false;
        }

        public int EventsQueuedCount()
        {
            return q.Size();
        }

        public bool IsEmpty()
        {
            return EventsQueuedCount() == 0 && !inAction;
        }
    }
}
