using System;

using Moq;
using NUnit.Framework;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;
using Naovigate.Util;

using Naovigate.Test.Communication;
using Naovigate.Test.Event;

namespace Naovigate.Test.Event.GoalToNao
{
    /// <summary>
    /// A test-suite for testing of the PickupEvent class.
    /// </summary>
    [TestFixture, Timeout(12000)]
    public class PickupEventTest
    {
        private static int ExpectedID = 43;
        private GoalComsStub goalComs;
        private CommunicationStream inputStream;
        private PickupEvent pickupEvent;

        [TestFixtureSetUp]
        public void initOnce()
        {
            inputStream = EventTestingUtilities.BuildStream();
            goalComs = new GoalComsStub(null);
        }

        [SetUp]
        public void Init()
        {
            inputStream = EventTestingUtilities.BuildStream(ExpectedID);
            goalComs.SetStream(inputStream);
            pickupEvent = new PickupEvent();
        }

        [TearDown]
        public void TearDown()
        {
            if (NaoState.Instance.Connected)
            {
                Walk.Instance.StopMove();
                NaoState.Instance.Disconnect();
            }
        }

        [Test]
        public void UnpackTest()
        {
            int id = (int)EventTestingUtilities.GetInstanceField(typeof(PickupEvent), pickupEvent, "id");
            Assert.AreEqual(id, ExpectedID);
        }

        [Test, Timeout(10000)]
        public void FireTest()
        {
            EventTestingUtilities.RequireWebots();
            EventQueue.Goal.Suspend();
            Type[] expectedResults = new Type[2] {typeof(SuccessEvent), typeof(FailureEvent)};
            EventQueue.Nao.Post(pickupEvent);
            PriorityQueue<INaoEvent> q = (PriorityQueue<INaoEvent>)EventTestingUtilities.GetInstanceField(typeof(EventQueue), EventQueue.Goal, "q");
            Walk.Instance.StopMove();
            System.Threading.Thread.Sleep(2500);
            // stopped move, should have gotten FailureEvent
            Assert.Contains(q.Dequeue().GetType(), expectedResults);
        }

        public void AbortTest()
        {
            EventTestingUtilities.RequireWebots();
            pickupEvent.Fire();
            pickupEvent.Abort();
            Assert.IsTrue(EventQueue.Nao.IsEmpty());
        }
    }
}