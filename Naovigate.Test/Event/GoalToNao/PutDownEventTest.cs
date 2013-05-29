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

namespace Naovigate.Test.Event.GoalToNao
{
    /// <summary>
    /// A test-suite for testing of the PutDownEvent class.
    /// </summary>
    [TestFixture]
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
        /// Ensures a clean environment for any upcoming tests.s
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            NaoState.Instance.Disconnect();
            EventQueue.Nao.Clear();
            EventQueue.Goal.Clear();
        }

        /// <summary>
        /// Executes the event succesfully. Expects a SuccessEvent to be present in the queue.
        /// </summary>
        [Test, Timeout(20000)]
        public void ValidFireTest()
        {
            EventTestingUtilities.RequireWebots();
            
            Mock<Grabber> mock = new Mock<Grabber>();
            Type expected = typeof(SuccessEvent);
            mock.Setup(m => m.HoldingObject()).Returns(false);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<SuccessEvent>(EventQueue.Goal.Peek(),
                "The Nao is not holding the object anymore, therefore the execution was a success.");
        }

        /// <summary>
        /// Executes the event unsuccesfully. Expects a FailureEvent to be present in the queue.
        /// </summary>
        [Test, Timeout(20000)]
        public void InvalidFireTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>();
            Type expected = typeof(FailureEvent);
            mock.Setup(m => m.HoldingObject()).Returns(true);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(), 
                "The Nao is still holding the object, therefore the execution was a failure");

            //Clean-up
            Grabber.Instance = new Grabber();
        }

        /// <summary>
        /// Executes and then aborts the event succesfully. Expects no new events in the queue.
        /// </summary>
        [Test, Timeout(3000)]
        public void ValidAbortTest()
        {
            EventTestingUtilities.RequireWebots();

            EventQueue.Goal.Suspend();
            EventQueue.Nao.SubscribeFire(naoEvent => naoEvent.Abort());
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsTrue(EventQueue.Nao.IsEmpty(),
                "The event was executed and aborted succesfully. There should be no event present in the queue.");

            //Clean-up
            EventQueue.Nao.UnsubscribeAll();
        }

        /// <summary>
        /// Executes and then aborts the event unsuccessfully. Expects an ErrorEvent in the queue.
        /// </summary>
        [Test, Timeout(3000)]
        public void InvalidAbortTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>();
            mock.Setup(m => m.Abort()).Throws(new Exception());
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.SubscribeFire(naoEvent => naoEvent.Abort());
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<ErrorEvent>(EventQueue.Goal.Peek(),
                "The event was executed and unsuccesfully aborted. There should be an ErrorEvent present in the queue.");

            //Clean-up
            EventQueue.Nao.UnsubscribeAll();
            Grabber.Instance = new Grabber();
        }
    }
}
