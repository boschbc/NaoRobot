using System;

using NUnit.Framework;
using System.Runtime.ExceptionServices;
using Naovigate.Movement;
using Naovigate.Event;
using System.Drawing;
using Naovigate.Event.GoalToNao;
using System.Collections.Generic;
using Naovigate.Communication;
using Naovigate.Test.Communication;

namespace Naovigate.Test.Event.GoalToNao
{
    [TestFixture, Timeout(12000)]
    public class GoToEventTest
    {
        private GoToEvent goToEvent;
        private GoalComsStub goalComs;
        private CommunicationStream emptyStream;

        [TestFixtureSetUp]
        public void initOnce()
        {
            emptyStream = EventTestingUtilities.BuildStream();
            goalComs = new GoalComsStub(emptyStream);
        }

        [SetUp]
        public void Init()
        {
            goToEvent = new GoToEvent(new Point(1, 1));
        }

        [TearDown]
        public void TearDown()
        {
            goalComs.SetStream(emptyStream);
            EventTestingUtilities.DisconnectWebots();
        }

        [Test]
        public void UnpackTest()
        {
            CommunicationStream stream = EventTestingUtilities.BuildStream(
                1, 15, 15
            );
            goalComs.SetStream(stream);
            goToEvent = new GoToEvent();
            List<Point> nodes = (List<Point>)EventTestingUtilities.GetInstanceField(typeof(GoToEvent), goToEvent, "locations");
            Assert.AreEqual(new Point(1, 1), nodes[0]);
        }

        [Test, HandleProcessCorruptedStateExceptions]
        public void FireTest()
        {
            EventTestingUtilities.RequireWebots();
            EventQueue.Nao.Post(goToEvent);
            EventQueue.Nao.WaitFor();
            System.Threading.Thread.Sleep(500);
            Assert.IsTrue(Walk.Instance.IsMoving());
        }
    }
}
