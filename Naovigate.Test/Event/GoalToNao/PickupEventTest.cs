using System;
using System.IO;
using System.Reflection;

using Moq;
using NUnit.Framework;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

using Naovigate.Test.Communication;
using Naovigate.Test.Event;

namespace Naovigate.Test.Event.GoalToNao
{
    /*
     * A test-suite for testing of the PickupEvent class.
     */
    [TestFixture, Timeout(2500)]
    public class PickupEventTest
    {
        private static int ExpectedID = 43;
        private GoalComsStub goalComs;
        private CommunicationStream inputStream;
        private PickupEvent pickupEvent;
     
        [TestFixtureSetUp]
        public void initOnce()
        {
            goalComs = new GoalComsStub();
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
            int id = (int)EventTestingUtilities.GetInstanceField(typeof(PickupEvent), pickupEvent, "id");
            Assert.AreEqual(id, ExpectedID);
        }

        [Test]
        public void FireTest()
        {
            Type[] expectedResults = new Type[2] {typeof(SuccessEvent), typeof(FailureEvent)};
            PriorityQueue<INaoEvent> q = (PriorityQueue<INaoEvent>)EventTestingUtilities.GetInstanceField(typeof(EventQueue), EventQueue.Instance, "q");
            pickupEvent.Fire();
            Assert.Contains(q.Dequeue(), expectedResults);
        }

        [Test]
        public void AbortTest()
        {
            //TODO
            Assert.IsTrue(false);
        }
    }
}