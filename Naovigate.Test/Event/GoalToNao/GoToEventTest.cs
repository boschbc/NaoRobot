using System;
using NUnit.Framework;
using Naovigate.Movement;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Communication;
using Naovigate.Test.Communication;

namespace Naovigate.Test.Event.GoalToNao
{
    [TestFixture, Timeout(2500)]
    public class GoToEventTest
    {
        private GoToEvent goToEvent;
        private GoalComsStub goalComs;
        private CommunicationStream emptyStream;

        private static readonly int theta = 0;
        private static readonly int markerID = 143;
        private static readonly int distance = 0;

        [TestFixtureSetUp]
        public void initOnce()
        {
            emptyStream = EventTestingUtilities.BuildStream();
            goalComs = new GoalComsStub(emptyStream);
        }

        [SetUp]
        public void Init()
        {
            goToEvent = new GoToEvent(theta, markerID, distance);
        }

        [TearDown]
        public void TearDown()
        {
            goalComs.SetStream(emptyStream);
        }

        [Test]
        public void UnpackTest()
        {
            CommunicationStream stream = EventTestingUtilities.BuildStream(
                GoToEventTest.theta,
                GoToEventTest.markerID,
                GoToEventTest.distance
            );
            goalComs.SetStream(stream);
            goToEvent = new GoToEvent();
            int theta = (int)EventTestingUtilities.GetInstanceField(typeof(GoToEvent), goToEvent, "theta");
            int markerID = (int)EventTestingUtilities.GetInstanceField(typeof(GoToEvent), goToEvent, "markerID");
            int distance = (int)EventTestingUtilities.GetInstanceField(typeof(GoToEvent), goToEvent, "distance");
            Assert.AreEqual(GoToEventTest.theta, theta);
            Assert.AreEqual(GoToEventTest.markerID, markerID);
            Assert.AreEqual(GoToEventTest.distance, distance);
        }

        [Test]
        public void FireTest()
        {
            EventQueue.Instance.Post(goToEvent);
            while(!EventQueue.Instance.IsEmpty()) System.Threading.Thread.Sleep(100);
            Assert.IsTrue(Walk.Instance.IsMoving());
        }
    }
}
