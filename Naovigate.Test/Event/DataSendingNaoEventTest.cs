using System;
using Naovigate.Communication;
using NUnit.Framework;
using Naovigate.Event.NaoToGoal;
using Naovigate.Test.Communication;
using System.IO;

namespace Naovigate.Test.Event
{
    public class DataSendingNaoEventTest
    {
        private GoalComsStub coms;
        private CommunicationStream stream;
        private MemoryStream mem;

        [SetUp]
        public void SetUp()
        {
            mem = new MemoryStream();
            stream = new CommunicationStream(mem);
            coms = new GoalComsStub(stream);
        }

        private void StartRead()
        {
            mem.Position = 0;
        }

        [Test]
        public void SendIDOnlyTest()
        {
            DataSendingNaoEvent e = new DataSendingNaoEvent(0xFF);
            e.SendAsInt();
            StartRead();
            Assert.AreEqual(1, mem.Length);
            byte val = stream.ReadByte();
            Assert.AreEqual(0xFF, val);
        }

        [Test]
        public void SendOneParamIntTest()
        {
            DataSendingNaoEvent e = new DataSendingNaoEvent(0xFF, 1);
            e.SendAsInt();
            StartRead();
            byte val = stream.ReadByte();
            Assert.AreEqual(0xFF, val);

            int intVal = stream.ReadInt();
            Assert.AreEqual(1, intVal);
        }

        [Test]
        public void SendMultipleParamsIntTest()
        {
            DataSendingNaoEvent e = new DataSendingNaoEvent(0xFF, 0, 1, 2, 3, 4);
            e.SendAsInt();
            StartRead();
            byte val = stream.ReadByte();
            Assert.AreEqual(0xFF, val);

            for (int i = 0; i <= 4;i++ )
            {
                Assert.AreEqual(i, stream.ReadInt());
            }
        }

        [Test]
        public void SendOneParamByteTest()
        {
            DataSendingNaoEvent e = new DataSendingNaoEvent(0xFF, 1);
            e.SendAsByte();
            StartRead();
            byte val = stream.ReadByte();
            Assert.AreEqual(0xFF, val);

            val = stream.ReadByte();
            Assert.AreEqual(1, val);
        }

        [Test]
        public void SendMultipleParamsByteTest()
        {
            DataSendingNaoEvent e = new DataSendingNaoEvent(0xFF, 0, 1, 2, 3, 4);
            e.SendAsByte();
            StartRead();
            byte val = stream.ReadByte();
            Assert.AreEqual(0xFF, val);

            for (int i = 0; i <= 4; i++)
            {
                Assert.AreEqual(i, stream.ReadByte());
            }
        }
    }
}
