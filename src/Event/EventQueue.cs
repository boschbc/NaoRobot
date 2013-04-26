using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Util;
using System.Threading;
using System.ComponentModel;
namespace Naovigate.Event
{
    class EventQueue
    {

        private static EventQueue instance;
        private PriorityQueue<INaoEvent> q;

        public EventQueue()
        {
            q = new PriorityQueue<INaoEvent>(3);
            Thread t = new Thread(new ThreadStart(Run));
            t.IsBackground = true;
            t.Start();
        }

        public void Enqueue(INaoEvent e)
        {
            // todo: synchronize, priority
            //lock (q)
            //{
                q.Enqueue(e, (int)e.GetPriority());
                //Monitor.Pulse(this);
            //}
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
                //Console.WriteLine("EventQueue waiting");
                Thread.Sleep(1000);
                //Monitor.Wait(this);
                //Console.WriteLine("Locking list");
                //lock (q)
                //{
                Console.WriteLine("EventQueue...");
                    if (!q.IsEmpty())
                    {
                        INaoEvent e = q.Dequeue();
                        Console.WriteLine("Fired " + e);
                        e.Fire();
                    }
                //}
            }
        }
    }
}
