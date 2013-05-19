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
            EventQueue.Instance.Post(haltEvent);
            // check not moving
            Assert.IsFalse(Walk.Instance.IsMoving());

            //TODO test without real robot
        }

        [Test]
        public void AbortTest()
        {
            //TODO finnish
            //TODO set Walk.IsMoving to true
            EventQueue.Instance.Suspend();
            EventQueue.Instance.Post(haltEvent);
            haltEvent.Abort();
            EventQueue.Instance.Resume();
            Assert.IsTrue(Walk.Instance.IsMoving());
        }
    }
}
