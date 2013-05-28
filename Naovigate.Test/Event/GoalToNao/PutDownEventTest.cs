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
        //private static int ExpectedID = 43;
        private GoalComsStub goalComs;
        //private CommunicationStream inputStream;
        private PutDownEvent putdownEvent;

        [TestFixtureSetUp]
        public void initOnce()
        {
            goalComs = new GoalComsStub(null);
        }

        [SetUp]
        public void Init()
        {
            //inputStream = EventTestingUtilities.BuildStream(ExpectedID);
            //goalComs.SetStream(inputStream);
            putdownEvent = new PutDownEvent();
        }

        [TearDown]
        public void TearDown()
        {
            if (NaoState.Instance.Connected)
            {
                Walk.Instance.StopMove();
                //NaoState.Instance.Disconnect();
            }
        }

        [Test]
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
            
            Assert.AreEqual(EventQueue.Goal.Peek().GetType(), expected);
        }

        [Test]
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

            Assert.AreEqual(EventQueue.Goal.Peek().GetType(), expected);
        }

        public void AbortTest()
        {
            EventTestingUtilities.RequireWebots();
            putdownEvent.Fire();
            putdownEvent.Abort();
            Assert.IsTrue(EventQueue.Nao.IsEmpty());
        }
    }
}
