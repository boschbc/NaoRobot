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
        /// Ensures a clean environment for any upcoming tests.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            //NaoState.Instance.Disconnect();
            if (NaoState.Instance.Connected)
                Grabber.Instance = new Grabber();
            EventQueue.Nao.Clear();
            EventQueue.Goal.Clear();
            EventQueue.Nao.UnsubscribeAll();
        }

        /// <summary>
        /// Execute the event while the Nao is not holding any object.
        /// Expects a FailureEvent.
        /// </summary>
        [Test, Timeout(20000)]
        public void ObjectAbsentTest()
        {
            EventTestingUtilities.RequireWebots();
            
            Mock<Grabber> mock = new Mock<Grabber>();
            mock.Setup(m => m.HoldingObject()).Returns(false);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                "The Nao was not holding an object, therefore there is nothing to put down.");

            //Clean-up
            //Grabber.Instance = new Grabber();
        }

        /// <summary>
        /// Execute the event unsuccesfully, but the object did get dropped.
        /// Expect a SuccessEvent.
        /// </summary>
        [Test, Timeout(20000)]
        public void InvalidFireObjectDroppedTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>();
            /// The first call should be true (called before the putdown, second call 
            /// should be false (called after the putdown).
            int callCounter = 0;
            mock.Setup(m => m.HoldingObject()).Returns(() => callCounter == 0).Callback(() => callCounter++);
            mock.Setup(m => m.PutDown()).Throws(new Exception());
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<SuccessEvent>(EventQueue.Goal.Peek(), 
                @"The Nao caught an exception while trying to put the object down,
                but it still dropped the object.");

            //Clean-up
            //Grabber.Instance = new Grabber();
        }

        /// <summary>
        /// Execute the event unsuccesfully, the object is still held.
        /// Expect a FailureEvent.
        /// </summary>
        [Test, Timeout(20000)]
        public void InvalidFireObjectHeldTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>();
            mock.Setup(m => m.HoldingObject()).Returns(true);
            mock.Setup(m => m.PutDown()).Throws(new Exception());
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                @"The Nao caught an exception while trying to put the object down,
                and still holds it at the end.");

            //Clean-up
            //Grabber.Instance = new Grabber();
        }

        /// <summary>
        /// Executes the event, while injecting a lethal error.
        /// Expect an ErrorEvent.
        /// </summary>
        [Test, Timeout(20000)]
        public void InternalErrorTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>();
            mock.Setup(m => m.HoldingObject()).Throws(new Exception());
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<ErrorEvent>(EventQueue.Goal.Peek(),
                "There is a serious internal error in Grabber.HoldingObject.");

            //Clean-up
            //EventQueue.Nao.UnsubscribeAll();
        }

        /// <summary>
        /// Executes the event succesfully.
        /// Expects a SuccessEvent.
        /// </summary>
        [Test, Timeout(20000)]
        public void ValidFireTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>();
            int callCounter = 0;
            mock.Setup(m => m.HoldingObject()).Returns(() => callCounter == 0).Callback(() => callCounter++);
            Grabber.Instance = mock.Object;

            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<SuccessEvent>(EventQueue.Goal.Peek(),
                "The event was executed succesfully.");
        }

        /// <summary>
        /// Executes the event and immediatly aborts. The Nao still holds
        /// the object.
        /// Expects a FailureEvent. 
        /// </summary>
        [Test, Timeout(20000)]
        public void AbortObjectDroppedTest()
        {
            EventTestingUtilities.RequireWebots();

            Mock<Grabber> mock = new Mock<Grabber>();
            mock.Setup(m => m.HoldingObject()).Returns(true);
            Grabber.Instance = mock.Object;

            EventQueue.Nao.SubscribeFire(naoEvent => naoEvent.Abort());
            EventQueue.Goal.Suspend();
            EventQueue.Nao.Post(putdownEvent);
            EventQueue.Nao.WaitFor();

            Assert.IsInstanceOf<FailureEvent>(EventQueue.Goal.Peek(),
                "The event was aborted before the Nao could drop the object.");
        }
    }
}
