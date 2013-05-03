using System;
using System.IO;

using NUnit.Framework;

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
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)NaoEventFactory.ActionCode.Move, 
                                moveCommand);
            Assert.IsInstanceOf(typeof(MoveNaoEvent), result);
        }

        [Test]
        public void NewEventValidLookTest()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)NaoEventFactory.ActionCode.Look,
                                lookCommand);
            Assert.IsInstanceOf(typeof(LookNaoEvent), result);
        }

        [Test]
        public void NewEventValidGrabTest()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)NaoEventFactory.ActionCode.Grab,
                                grabCommand);
            Assert.IsInstanceOf(typeof(GrabNaoEvent), result);
        }

        [Test]
        [ExpectedException(typeof(InvalidActionCodeException))]
        public void NewEventInvalidExceptionThrown()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                invalidActionCode,
                                invalidCommand);
        }
    }
}
