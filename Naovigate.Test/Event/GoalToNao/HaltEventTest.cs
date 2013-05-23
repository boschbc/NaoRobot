using System;
using NUnit.Framework;
using Naovigate.Movement;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Communication;
namespace Naovigate.Test.Event.GoalToNao
{
    [TestFixture, Timeout(2500)]
    public class HaltEventTest
    {
        private HaltEvent haltEvent;

        [SetUp]
        public void Init()
        {
            haltEvent = new HaltEvent();
        }

        [Test]
        public void FireTest()
        {
            EventTestingUtilities.RequireWebots();
            EventQueue.Nao.Post(haltEvent);
            // check not moving
            Assert.IsFalse(Walk.Instance.IsMoving());
        }

        [Test]
        public void AbortTest()
        {
            EventTestingUtilities.RequireWebots();
            Walk.Instance.StartWalking(1, 1, 0);
            EventQueue.Nao.Suspend();
            EventQueue.Nao.Post(haltEvent);
            haltEvent.Abort();
            EventQueue.Nao.Resume();
            Assert.IsTrue(Walk.Instance.IsMoving());
        }
    }
}
