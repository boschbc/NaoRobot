﻿using System;
using System.IO;
using Naovigate.Communication;
using NUnit.Framework;

namespace Naovigate.Test.Communication
{
    [TestFixture, Timeout(1000)]
    abstract class AbstractCommunicationStreamTest
    {
        protected ICommunicationStream stream;
        protected MemoryStream internalStream;

        [SetUp]
        public void Setup()
        {
            internalStream = new MemoryStream();
            Initialize();
        }

        public abstract void Initialize();

        /*
         * Set the stream ready for reading
         */
        protected void StartRead()
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
        public void ReadByteTest()
        {
            byte[] buffer = { 3 };
            stream.Write(buffer);
            StartRead();
            byte x = stream.ReadByte();
            Assert.AreEqual(3, x);
        }

        [Test]
        public void BufferDataTest()
        {
            byte[] data = {1, 2, 3};
            byte[] data2 = {4, 5, 6};
            byte[] data3 = {7, 8, 9};
            stream.Write(data);
            stream.Open = false;
            stream.Write(data2);
            stream.InternalStream = internalStream;
            stream.Write(data3);
            StartRead();
            for (int i = 1; i <= 9;i++ )
            {
                Assert.AreEqual(i, stream.ReadByte());
            }
        }

        [Test]
        public void BufferDataOverlappingBlocksTest()
        {
            byte[] data = { 1, 2, 3 };
            byte[] data2 = { 4, 5, 6 };
            byte[] data3 = { 7, 8, 9 };
            stream.Write(data);
            stream.Open = false;
            stream.Write(data2);
            stream.InternalStream = internalStream;
            stream.Write(data3);
            StartRead();
            byte[] all = new byte[9];
            byte[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            stream.Read(all);
            Assert.AreEqual(expected, all);
        }

        [Test]
        public void ReadWriteStringTest()
        {
            if (!stream.CanReadString || !stream.CanWriteString) Assert.Pass();
            string data = "hello world";
            stream.WriteString(data);
            stream.WriteNewline();
            StartRead();
            string res = stream.ReadString();
            Assert.AreEqual(data, res);
        }

        [Test]
        public void WriteStringTest()
        {
            if (!stream.CanWriteString) Assert.Pass();
            string data = new string(new char[] { '0', '1', '2' });
            stream.WriteString(data);
            StartRead();
            byte[] buf = new byte[internalStream.Length];
            if(internalStream.Read(buf, 0, buf.Length) != buf.Length){
                Assert.Inconclusive();
            }
            Assert.AreEqual('0', buf[0]);
            Assert.AreEqual('1', buf[1]);
            Assert.AreEqual('2', buf[2]);
        }

        [Test]
        public void ReadStringTest()
        {
            if (!stream.CanReadString) Assert.Pass();
            string data = "hello world";
            byte[] bin = System.Text.Encoding.UTF8.GetBytes(data + "\n");
            internalStream.Write(bin, 0, bin.Length);
            StartRead();
            Assert.AreEqual(data, stream.ReadString());
        }

        [Test]
        public void ReadWriteNewlineTest()
        {
            if (!stream.CanReadString) Assert.Pass();
            stream.WriteNewline();
            StartRead();
            string res = stream.ReadString();

            // ReadString consumes the newline, so we expect nothing.
            Assert.AreEqual("", res);
        }

        [Test]
        public void WriteNewlineTest()
        {
            try
            {
                stream.WriteNewline();
                StartRead();
                int res = internalStream.ReadByte();
                Assert.AreEqual('\n', res);
            } catch(NotSupportedException){
                Assert.Pass();
            }
        }

        [Test]
        public void GetStreamTest()
        {
            Assert.AreEqual(internalStream, stream.InternalStream);
        }

        [TearDown]
        public void TearDown()
        {
            internalStream.Close();
        }
    }
}
