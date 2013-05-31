using System;

using Moq;
using NUnit.Framework;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
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
            goalComs = new GoalComsStub(null);
        }

        [SetUp]
        public void Init()
        {
            Logger.Clear();
            inputStream = EventTestingUtilities.BuildStream(ExpectedID);
            goalComs.SetStream(inputStream);
            pickupEvent = new PickupEvent();
        }

        [TearDown]
        public void TearDown()
        {
            NaoState.Instance.Disconnect();
            EventQueue.Nao.Clear();
            EventQueue.Goal.Clear();
            EventQueue.Nao.UnsubscribeAll();
        }

        /// <summary>
        /// Build a new event.
        /// Expect the correct Object-ID has been extracted out of the stream.
        /// </summary>
        [Test]
        public void UnpackTest()
        {
            Assert.AreEqual(pickupEvent.ObjectID, ExpectedID);
        }

        /// <summary>
        /// Execute a pickup event while the Nao is already holding an object.
        /// Expects a FailureEvent.
        /// </summary>
        [Test]
        public void ObjectAlreadyHeldTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Returns(true);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(pickupEvent);
            pickupEvent.WaitFor();

            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                "The Nao is already holding an object, therefore cannot pick up a new one.");
        }

        /// <summary>
        /// Executes a pickup event, but at the end the Nao is not holding any object.
        /// Expect a FailureEvent.
        /// </summary>
        [Test]
        public void ValidFireObjectNotHeldTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Returns(false);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(pickupEvent);
            pickupEvent.WaitFor();

            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                "The event was executed but the Nao is not holding anything.");
        }

        /// <summary>
        /// Executes a pickup event successfully.
        /// Expect a SuccessEvent.
        /// </summary>
        [Test]
        public void ValidFireObjectHeldTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            int callCounter = 0;
            mock.Setup(m => m.HoldingObject())
                .Returns(() => callCounter > 0)
                .Callback(() => callCounter++);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(pickupEvent);
            pickupEvent.WaitFor();

            Assert.IsInstanceOf<SuccessEvent>(EventQueue.Goal.Peek(),
                "The event was successfully executed.");
        }
        
        /// <summary>
        /// Executes the event with an exception.
        /// Expect a FailureEvent.
        /// </summary>
        [Test]
        public void InternalErrorTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Throws(new Exception());
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(pickupEvent);
            pickupEvent.WaitFor();

            Assert.IsInstanceOf<ErrorEvent>(EventQueue.Goal.Peek(),
                "An internal error has been encountered, expecting an ErrorEvent.");
        }

        /// <summary>
        /// Executes the event and immediatly aborts.
        /// Expects a FailureEvent.
        /// </summary>
        [Test]
        public void AbortTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Returns(false);

            //TODO
        }
    }
}