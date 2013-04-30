﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using Naovigate.Event;



namespace Naovigate.Testing{
	// test if events are fired
	// test if events are fired in order
	// test if events are fired according to priority
	class EventQueueTest{
        private Tracker t;
        private EventQueue q;
		private void Add(params INaoEvent[] events){
		    q.Enqueue(events);
		}

        private void WaitFor()
        {
            // after event queue is empty, it is still processing the last event
            //while (q.EventsQueuedCount() > 0) ;
            //Thread.Sleep(100);

            while (!q.IsEmpty()) ;
        }

        [TestFixtureSetUp]
        public void SetupOnce()
        {
            Console.WriteLine("New EventQueue");
            q = new EventQueue();
        }

        [SetUp]
        public void Setup(){
            Console.WriteLine("New Tracker");
            t = new Tracker();
        }

		[Test]
		public void SingleEventFiredTest(){
			Add(new TEvent(t,Priority.Low));
            WaitFor();
            Assert.AreEqual(t.events.Count, 1);
		}

		[Test]
		public void MultipleEventFiredTest(){
			Add(new TEvent(t,Priority.Low),
					new TEvent(t, Priority.Low),
					new TEvent(t, Priority.Low),
					new TEvent(t, Priority.Low),
					new TEvent(t, Priority.Low));
            WaitFor();
            Assert.AreEqual(t.events.Count, 5);
		}

		[Test]
		public void FiredInOrderTest(){
            Add(new TEvent(t, Priority.Low),
                    new TEvent(t, Priority.High),
                    new TEvent(t, Priority.Medium),
                    new TEvent(t, Priority.Low),
                    new TEvent(t, Priority.Low));
            WaitFor();
            Assert.AreEqual(5, t.events.Count());
            Assert.AreEqual(Priority.High, t.events[0].GetPriority());
            Assert.AreEqual(Priority.Medium, t.events[1].GetPriority());
            Assert.AreEqual(Priority.Low, t.events[2].GetPriority());
            Assert.AreEqual(Priority.Low, t.events[3].GetPriority());
            Assert.AreEqual(Priority.Low, t.events[4].GetPriority());
		}
	}

    class Tracker{
		public List<INaoEvent> events;
		public bool ended = false;
		public Tracker(){
		    events = new List<INaoEvent>();
        }

	    public void Fired(INaoEvent e){
		    events.Add(e);
	    }
    }

    class TEvent : INaoEvent{
		private Tracker t;
		private Priority p;

		public TEvent(Tracker t, Priority p){
			this.t = t;
			this.p = p;
		}

        public void SetPriority(Priority p)
        {
            this.p = p;
        }

		public Priority GetPriority(){
			return p;
		}

		public virtual void Fire(){
			t.Fired(this);
		}
	}
}