﻿using System;
using System.IO;
using System.Reflection;

using Moq;
using NUnit.Framework;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Event.NaoToGoal;
using Naovigate.Haptics;
using Naovigate.Movement;
using Naovigate.Util;

using Naovigate.Test.Communication;
using Naovigate.Test.Event;

namespace Naovigate.Test.Event.GoalToNao
{
    /// <summary>
    /// A test-suite for testing of the PickupEvent class.
    /// </summary>
    [TestFixture]
    public class PickupEventTest
    {
        private static int ExpectedID = 43;
        private GoalComsStub goalComs;
        private CommunicationStream inputStream;
        private PickupEvent pickupEvent;
        
        /// <summary>
        /// Uses reflection to get the field value from an object. Credit goes to user "dcp" on StackOverflow.
        /// </summary>
        ///
        /// <param name="type">The instance type.</param>
        /// <param name="instance">The instance object.</param>
        /// <param name="fieldName">The field's name which is to be fetched.</param>
        ///
        /// <returns>The field value from the object.</returns>
        private static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }

        [TestFixtureSetUp]
        public void initOnce()
        {
            goalComs = new GoalComsStub(null);
        }

        [SetUp]
        public void Init()
        {
            inputStream = EventTestingUtilities.BuildStream(ExpectedID);
            goalComs.SetStream(inputStream);
            pickupEvent = new PickupEvent();
        }

        [Test]
        public void UnpackTest()
        {
            int id = (int) GetInstanceField(typeof(PickupEvent), pickupEvent, "id");
            Assert.AreEqual(id, ExpectedID);
        }

        [Test]
        public void FireTest()
        {
            EventTestingUtilities.RequireWebots();

            Type[] expectedResults = new Type[2] {typeof(SuccessEvent), typeof(FailureEvent)};
            pickupEvent.Fire();
            PriorityQueue<INaoEvent> q = (PriorityQueue<INaoEvent>)GetInstanceField(typeof(EventQueue), EventQueue.Instance, "q");
            Assert.Contains(q.Dequeue(), expectedResults);
        }

        [Test]
        public void AbortTest()
        {
            EventTestingUtilities.RequireWebots();
            pickupEvent.Fire();
            pickupEvent.Abort();
            PriorityQueue<INaoEvent> q = (PriorityQueue<INaoEvent>)GetInstanceField(typeof(EventQueue), EventQueue.Instance, "q");
            Assert.IsTrue(q.IsEmpty());
        }
    }
}