using System;
using NUnit.Framework;
using Naovigate.Movement;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Communication;
using Naovigate.Util;
namespace Naovigate.Test.Event.GoalToNao
{
    [TestFixture, Timeout(12000)]
    public class HaltEventTest
    {
        private HaltEvent haltEvent;

        [SetUp]
        public void Init()
        {
            haltEvent = new HaltEvent();
        }

        [TearDown]
        public void TearDown()
        {
            EventTestingUtilities.DisconnectWebots();
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
            System.Threading.Thread.Sleep(500);
            Assert.IsTrue(Walk.Instance.IsMoving());
        }
    }
}
