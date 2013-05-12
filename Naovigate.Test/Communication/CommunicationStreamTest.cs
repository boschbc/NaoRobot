using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Naovigate.Communication;
using NUnit.Framework;

namespace Naovigate.Testing
{
    class CommunicationStreamTest
    {
        private CommunicationStream stream;
        private MemoryStream internalStream;

        [SetUp]
        public void Setup()
        {
            internalStream = new MemoryStream();
            stream = new CommunicationStream(internalStream);
        }

        /*
         * Set the stream ready for reading
         */
        private void StartRead()
        {
            internalStream.Seek(0, SeekOrigin.Begin);
        }

        [Test]
        public void ReadWriteIntSingleByteValueTest()
        {
            int x = 32;
            stream.WriteInt(x);
            StartRead();
            int y = stream.ReadInt();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWriteIntMultipleByteValueTest()
        {
            int x = 3214;
            stream.WriteInt(x);
            StartRead();
            int y = stream.ReadInt();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWriteIntMultipleByteNegativeValueTest()
        {
            int x = -3214;
            stream.WriteInt(x);
            StartRead();
            int y = stream.ReadInt();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWriteIntMaxValueTest()
        {
            int x = int.MaxValue;
            stream.WriteInt(x);
            StartRead();
            int y = stream.ReadInt();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWriteLongSingleByteValueTest()
        {
            long x = 36;
            stream.WriteLong(x);
            StartRead();
            long y = stream.ReadLong();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWriteLongMultipleByteValueTest()
        {
            long x = 3814;
            stream.WriteLong(x);
            StartRead();
            long y = stream.ReadLong();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWriteLongMultipleByteNegativeValueTest()
        {
            long x = -3814;
            stream.WriteLong(x);
            StartRead();
            long y = stream.ReadLong();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWritelongMaxValueTest()
        {
            long x = long.MaxValue;
            stream.WriteLong(x);
            StartRead();
            long y = stream.ReadLong();
            Assert.AreEqual(x, y);
        }

        [Test]
        public void ReadWriteAllBufferTest()
        {
            byte[] buffer = {3, 5, 45, 255, 127, 3, 4, 0, 12, 42};
            stream.Write(buffer);
            StartRead();
            byte[] res = new byte[buffer.Length];
            int x = stream.Read(res);
            Assert.AreEqual(res.Length, x);
            Assert.AreEqual(buffer, res);
        }

        [Test]
        public void ReadWriteBufferWithOffsetTest()
        {
            byte[] buffer = { 3, 5, 45, 255, 127, 3, 4, 0, 12, 42 };
            byte[] res = { 3, 5, 45, 255, 0, 0, 0, 0, 0, 42 };
            stream.Write(buffer, 4, 5);//127, 3, 4, 0, 12
            StartRead();
            int x = stream.Read(res, 4, 5);
            Assert.AreEqual(5, x);
            Assert.AreEqual(buffer, res);
        }

        [Test]
        public void GetStreamTest()
        {
            Assert.AreEqual(internalStream, stream.Stream);
        }

        [TearDown]
        public void TearDown()
        {
            internalStream.Close();
        }
    }
}
