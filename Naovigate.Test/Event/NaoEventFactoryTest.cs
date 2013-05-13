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
                               (byte)EventCode.Exit);
            Assert.IsInstanceOf(typeof(ExitEvent), result);
        }

        [Test]
        public void NewPutDownEventTest()
        {
            goalComs.SetStream(putDownInputStream);
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.PutDown);
            Assert.IsInstanceOf(typeof(PutDownEvent), result);
        }

        [Test]
        public void NewGoToEventTest()
        {
            goalComs.SetStream(goToInputStream);
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.GoTo);
            Assert.IsInstanceOf(typeof(GoToEvent), result);
        }

        [Test]
        public void NewPickupEventTest()
        {
            goalComs.SetStream(pickupInputStream);
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.Pickup);
            Assert.IsInstanceOf(typeof(PickupEvent), result);
        }

        [Test]
        public void NewHaltEventTest()
        {
            goalComs.SetStream(haltInputStream);
            INaoEvent result = NaoEventFactory.NewEvent(
                                (byte)EventCode.Halt);
            Assert.IsInstanceOf(typeof(HaltEvent), result);
        }

        [Test]
        [ExpectedException(typeof(InvalidEventCodeException))]
        public void NewInvalidEventTest()
        {
            goalComs.SetStream(invalidInputStream);
            INaoEvent result = NaoEventFactory.NewEvent(
                                invalidEventCode);
        }
    }
}
