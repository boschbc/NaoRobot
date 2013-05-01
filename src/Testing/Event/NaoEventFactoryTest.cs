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

        private CommunicationStream StreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return new CommunicationStream(stream);
        }

        [SetUp]
        public void Init()
        {
            moveCommand = StreamFromString("0.5 0.2");
            grabCommand = StreamFromString("");
            lookCommand = StreamFromString("3.14");
            invalidCommand = StreamFromString("This is invalid.");
            invalidActionCode = 99;
        }

        [Test]
        public void NewMoveEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)NaoEventFactory.ActionCode.Move, 
                                moveCommand);
            Assert.IsInstanceOf(typeof(MoveNaoEvent), result);
        }

        [Test]
        public void NewLookEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)NaoEventFactory.ActionCode.Look,
                                lookCommand);
            Assert.IsInstanceOf(typeof(LookNaoEvent), result);
        }

        [Test]
        public void NewGrabEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)NaoEventFactory.ActionCode.Grab,
                                grabCommand);
            Assert.IsInstanceOf(typeof(GrabNaoEvent), result);
        }

        [Test]
        [ExpectedException(typeof(InvalidActionCodeException))]
        public void NewInvalidEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                invalidActionCode,
                                invalidCommand);
        }
    }
}
