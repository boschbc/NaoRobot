using System;
using System.IO;

using NUnit.Framework;

using Naovigate.Test.Communication;
using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.GoalToNao;

namespace Naovigate.Testing.Event
{
    /*
     * A test-suite for testing of the NaoEventFactory class.
     */
    [TestFixture]
    public class NaoEventFactoryTests
    {

        private GoalComsStub goalComs;
        private CommunicationStream exitInputStream;
        private CommunicationStream putDownInputStream;
        private CommunicationStream goToInputStream;
        private CommunicationStream pickupInputStream;
        private CommunicationStream haltInputStream;
        private CommunicationStream invalidInputStream;
        private byte invalidEventCode;

        /*
         * Creates a stream and fill it with data.
         */
        private CommunicationStream BuildStream(params int[] input)
        {
            MemoryStream mem = new MemoryStream();
            CommunicationStream com = new CommunicationStream(mem);

            foreach (int i in input)
            {
                com.WriteInt(i);
            }
            mem.Position = 0;  //Bring the seeker back to the start.
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

            int objectID = 43;  //Dummy
            exitInputStream = BuildStream();
            putDownInputStream = BuildStream();
            goToInputStream = BuildStream(objectID, 0);
            pickupInputStream = BuildStream(objectID);
            haltInputStream = BuildStream();
            invalidInputStream = BuildStream(-1);
            invalidEventCode = 99;
        }

        [Test]
        public void NewExitEventTest()
        {
            goalComs.SetStream(exitInputStream);
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.Exit,
                                exitInputStream);
            Assert.IsInstanceOf(typeof(ExitEvent), result);
        }

        [Test]
        public void NewPutDownEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.PutDown, 
                                putDownInputStream);
            Assert.IsInstanceOf(typeof(PutDownEvent), result);
        }

        [Test]
        public void NewEvent_Valid_GoToEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.GoTo,
                                goToInputStream);
            Assert.IsInstanceOf(typeof(GoToEvent), result);
        }

        [Test]
        public void NewEvent_Valid_PickupEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.Pickup,
                                pickupInputStream);
            Assert.IsInstanceOf(typeof(PickupEvent), result);
        }

        [Test]
        public void NewEvent_Valid_HaltEvent()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.Halt,
                                haltInputStream);
            Assert.IsInstanceOf(typeof(HaltEvent), result);
        }

        [Test]
        [ExpectedException(typeof(InvalidEventCodeException))]
        public void NewEvent_Invalid_ExceptionThrown()
        {
            INaoEvent result = NaoEventFactory.NewEvent(
                                invalidEventCode,
                                invalidInputStream);
        }
    }
}
