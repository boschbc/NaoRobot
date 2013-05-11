using System;
using System.IO;

using NUnit.Framework;

using Naovigate.Test.Communication;
using Naovigate.Communication;
using Naovigate.Event;

namespace Naovigate.Testing.Event
{
    /**
     * A test-suite for testing of the NaoEventFactory class.
     **/
    [TestFixture]
    public class NaoEventFactoryTests
    {
        private GoalComsStub goalComs;
        private CommunicationStream moveCommand;
        private CommunicationStream grabCommand;
        private CommunicationStream lookCommand;
        private CommunicationStream invalidCommand;
        private byte invalidActionCode;

        /**
         * create the stream and fill it with data
         */
        private CommunicationStream BuildStream(params int[] input)
        {
            MemoryStream mem = new MemoryStream();
            CommunicationStream com = new CommunicationStream(mem);

            foreach (int i in input)
            {
                com.WriteInt(i);
            }
            mem.Position = 0;
            return com;
        }

        [TestFixtureSetUp]
        public void initOnce()
        {
            goalComs = new GoalComsStub(null);
        }

        [SetUp]
        public void Init()
        {
            moveCommand = BuildStream(1, 1);
            grabCommand = BuildStream();
            lookCommand = BuildStream(3);
            invalidCommand = BuildStream(-1);
            invalidActionCode = 99;
        }

        [Test]
        public void NewEventValidMoveTest()
        {
            goalComs.SetStream(moveCommand);
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)NaoEventFactory.ActionCode.Move);
            Assert.IsInstanceOf(typeof(MoveNaoEvent), result);
        }

        [Test]
        [ExpectedException(typeof(InvalidActionCodeException))]
        public void NewEventInvalidExceptionThrown()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                invalidActionCode);
        }
    }
}
