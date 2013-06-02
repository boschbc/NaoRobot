using System;

using Moq;
using NUnit.Framework;

using Naovigate.Communication;
using Naovigate.Grabbing;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Event.NaoToGoal;
using Naovigate.Movement;
using Naovigate.Util;

using Naovigate.Test.Communication;
using Naovigate.Test.Event;
using System.Threading;

namespace Naovigate.Test.Event.GoalToNao
{
    /// <summary>
    /// A test-suite for testing of the PutDownEvent class.
    /// </summary>
    [TestFixture, Timeout(12000)]
    public class PutDownEventTest
    {
        private GoalComsStub goalComs;
        private CommunicationStream emptyStream;
        private PutDownEvent putdownEvent;

        /// <summary>
        /// Create a GoalCommunicator stub and a dummy communication stream for it.
        /// </summary>
        [TestFixtureSetUp]
        public void initOnce()
        {
            emptyStream = EventTestingUtilities.BuildStream();
            goalComs = new GoalComsStub(emptyStream);
        }

        /// <summary>
        /// Initialise the event.
        /// </summary>
        [SetUp]
        public void Init()
        {
            Logger.Clear();
            putdownEvent = new PutDownEvent();
        }

        /// <summary>
        /// Ensures a clean environment for any upcoming tests.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            EventTestingUtilities.DisconnectWebots();
            //if (NaoState.Instance.Connected)
            //    Grabber.Instance = new Grabber();
            EventQueue.Nao.Clear();
            EventQueue.Goal.Clear();
            EventQueue.Nao.UnsubscribeAll();
        }

        /// <summary>
        /// Execute the event while the Nao is not holding any object.
        /// Expects a FailureEvent.
        /// </summary>
        [Test]
        public void ObjectAbsentTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Returns(false);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            putdownEvent.WaitFor();
            
            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                "The Nao was not holding an object, therefore there is nothing to put down.");
        }

        /// <summary>
        /// Execute the event unsuccesfully, but the object did get dropped.
        /// Expect a SuccessEvent.
        /// </summary>
        [Test]
        public void InvalidFireObjectDroppedTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Returns(false);
            mock.Setup(m => m.PutDown()).Throws(new Exception());
            Grabber.Instance = mock.Object;
            
            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            putdownEvent.WaitFor();

            Assert.IsInstanceOf<SuccessEvent>(EventQueue.Goal.Peek(), 
                @"The Nao caught an exception while trying to put the object down,
                but it still dropped the object. Therefore, expect success.");
        }

        /// <summary>
        /// Execute the event unsuccesfully, the object is still held.
        /// Expect a FailureEvent.
        /// </summary>
        [Test]
        public void InvalidFireObjectHeldTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Returns(true);
            mock.Setup(m => m.PutDown()).Throws(new Exception());
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            putdownEvent.WaitFor();

            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                @"The Nao caught an exception while trying to put the object down,
                and still holds it at the end.");
        }

        /// <summary>
        /// Executes the event, while injecting a lethal error.
        /// Expect an ErrorEvent.
        /// </summary>
        [Test]
        public void InternalErrorTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Throws(new Exception());
            Grabber.Instance = mock.Object;
            
            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            putdownEvent.WaitFor();

            Assert.IsInstanceOf<ErrorEvent>(EventQueue.Goal.Peek(),
                "There is a serious internal error in Grabber.HoldingObject.");
        }

        /// <summary>
        /// Executes the event succesfully.
        /// Expects a SuccessEvent.
        /// </summary>
        [Test]
        public void ValidFireTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            int callCounter = 0;
            mock.Setup(m => m.HoldingObject())
                .Returns(() => callCounter == 0)
                .Callback(() => callCounter++);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            putdownEvent.WaitFor();

            Assert.IsInstanceOf<SuccessEvent>(EventQueue.Goal.Peek(),
                "The event was executed succesfully.");
        }

        /// <summary>
        /// Executes the event and immediatly aborts. The Nao still holds
        /// the object.
        /// Expects a FailureEvent. 
        /// </summary>
        [Test]
        public void AbortObjectHeldTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>() { CallBase = true };
            mock.Setup(m => m.HoldingObject()).Returns(true);
            Grabber.Instance = mock.Object;
            
            EventQueue.Goal.Suspend();
            EventQueue.Nao.SubscribeFire(naoEvent => naoEvent.Abort());
            EventQueue.Nao.Post(putdownEvent);
            putdownEvent.WaitFor();
            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                "The event was aborted before the Nao could drop the object.");
        }
    }
}
