﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Naovigate.Util;
using NUnit.Framework;

using Naovigate.Event;

namespace Naovigate.Test.Event
{	[TestFixture, Timeout(2500)]
	public class EventQueueTest{        private Tracker t;
        private EventQueue q;
		private void Add(params INaoEvent[] events){
		    q.Post(events);
		}

        /*
         * Wait until all the events are fired
         */
        private void WaitFor()
        {
            q.WaitFor();
            //while (!q.IsEmpty())
            //    Thread.Sleep(50);
        }

        [SetUp]
        public void Setup(){
            Console.WriteLine("New EventQueue");
            q = new EventQueue();
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
            Assert.AreEqual(Priority.High, t.events[0].Priority);
            Assert.AreEqual(Priority.Medium, t.events[1].Priority);
            Assert.AreEqual(Priority.Low, t.events[2].Priority);
            Assert.AreEqual(Priority.Low, t.events[3].Priority);
            Assert.AreEqual(Priority.Low, t.events[4].Priority);
		}

        [Test]
        public void FireMultipleSessionsTest()
        {
            Add(new TEvent(t, Priority.Low),
                    new TEvent(t, Priority.High),
                    new TEvent(t, Priority.Medium),
                    new TEvent(t, Priority.Low),
                    new TEvent(t, Priority.Low));
            WaitFor();
            Assert.AreEqual(5, t.events.Count());
            Add(new TEvent(t, Priority.Low),
                   new TEvent(t, Priority.High),
                   new TEvent(t, Priority.Medium),
                   new TEvent(t, Priority.Low),
                   new TEvent(t, Priority.Low));
            WaitFor();
            Assert.AreEqual(10, t.events.Count());
            Add(new TEvent(t, Priority.Low),
                  new TEvent(t, Priority.High),
                  new TEvent(t, Priority.Medium),
                  new TEvent(t, Priority.Low),
                  new TEvent(t, Priority.Low));
            WaitFor();
            Assert.AreEqual(15, t.events.Count());
        }
        
        [Test, Timeout(7500)]
        public void SuspendAndResumeTest()
        {
            Logger.Log(this, "SuspendAndResumeTest");
            q.Suspended = true;
            Logger.Log(this, "Suspended");
            Add(new TEvent(t, Priority.Low),
                    new TEvent(t, Priority.High),
                    new TEvent(t, Priority.Medium),
                    new TEvent(t, Priority.Low),
                    new TEvent(t, Priority.Low));
            Logger.Log(this, "Posted");
            Thread.Sleep(250);
            Logger.Log(this, "Slept");
            // none fired, queue is suspended
            Assert.AreEqual(0, t.events.Count());
            Logger.Log(this, "Asserted count == 0");
            q.Suspended = false;
            Logger.Log(this, "Resume");
            Logger.Log(this, "Waitfor");
            WaitFor();
            Logger.Log(this, "waited");

            // should all be fired as normal
            Assert.AreEqual(5, t.events.Count());
            Assert.AreEqual(Priority.High, t.events[0].Priority);
            Assert.AreEqual(Priority.Medium, t.events[1].Priority);
            Assert.AreEqual(Priority.Low, t.events[2].Priority);
            Assert.AreEqual(Priority.Low, t.events[3].Priority);
            Assert.AreEqual(Priority.Low, t.events[4].Priority);
        }

        [Test, Timeout(750000)]
        public void SuspendAndResumeALOTTest()
        {
            for (int i = 0; i < 100;i++ )
            {
                Setup();
                SuspendAndResumeTest();
            }
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
            this.ExecutionBehavior = ExecutionBehavior.Durative;
		}

        public ExecutionBehavior ExecutionBehavior
        {
            get;
            set;
        }

        public Priority Priority
        {
            get { return p; }
            set { p = value; }
        }

		public virtual void Fire(){
			t.Fired(this);
		}

        public void Abort()
        {

        }

        public EventCode EventCode
        {
            get { return EventCode.Exit; }
        }
	}
}