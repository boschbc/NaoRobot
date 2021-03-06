﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Naovigate.Util;

namespace Naovigate.Communication
{
    /// <summary>
    /// Ease the implementation for a ICommunicationStream.
    /// The following methods have to be implemented:
    ///     void Write(byte[], int, int)
    ///     int Read(byte[], int, int)
    ///     void WriteString(string)
    ///     string ReadString()
    ///     void FlushBuffer()
    /// </summary>
    public abstract class AbstractCommunicationStream : ICommunicationStream
    {
        public static bool timeoutEnabled = false;
        protected static readonly int timeoutInMs = 10000;

        private static readonly int ticksToMsRatio = 10000;
        protected static readonly int timeout = timeoutInMs * ticksToMsRatio;
        private Queue<byte> buffer;
        private Stream stream;
        private object wLock = new object();
        private object rLock = new object();

        /// <summary>
        /// Creates a new instance wrapped around a given stream.
        /// </summary>
        /// <param name="stream">The stream to wrap.</param>
        public AbstractCommunicationStream(Stream stream)
        {
            InternalStream = stream;
            buffer = new Queue<byte>();
        }

        /// <summary>
        /// Sets the timeout of this stream, if this stream supports timeouts.
        /// </summary>
        protected void InitStream()
        {
            if (timeoutEnabled && stream.CanTimeout)
            {
                stream.WriteTimeout = timeoutInMs;
                stream.ReadTimeout = timeoutInMs;
            }
        }

        /// <summary>
        /// Buffers given data. 
        /// </summary>
        /// <param name="data">Byte array.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to buffer.</param>
        protected void Buffer(byte[] data, int off, int len)
        {
            Logger.Log(this, "Buffering data.");
            for (int i = off; i < off + len; i++)
            {
                buffer.Enqueue(data[i]);
            }
        }

        /// <summary>
        /// Write to the stream.
        /// </summary>
        /// <param name="data">Byte array.</param>
        public void Write(byte[] data)
        {
            Write(data, 0, data.Length);
        }  
        
        /// <summary>
        /// Write a byte to the stream.
        /// </summary>
        /// <param name="x">A byte.</param>
        public void WriteByte(byte x)
        {
            WriteBytesFromValue(x, 1);
        }
        
        /// <summary>
        /// Write an integer to the stream.
        /// </summary>
        /// <param name="x">An integer.</param>
        public void WriteInt(int x)
        {
            WriteBytesFromValue(x, 4);
        }

        /// <summary>
        /// Write a long to the stream.
        /// </summary>
        /// <param name="x">A long.</param>
        public void WriteLong(long x)
        {
            WriteBytesFromValue(x, 8);
        }

        /// <summary>
        /// Retrieves the last len bytes of a given long and writes them to the stream.
        /// </summary>
        /// <param name="x">A long to be chunked.</param>
        /// <param name="len">The amount of bytes to chunk and write to the stream.</param>
        public void WriteBytesFromValue(long x, int len)
        {
            byte[] data = new byte[len];
            for (int i = len - 1; i >= 0; i--)
            {
                data[i] = (byte)(x & 0xFF);
                x = x >> 8;
            }
            Write(data);
        }

        /// <summary>
        /// Fill the buffer buf with data from the stream.
        /// </summary>
        /// <param name="buf">The buffer to be filled.</param>
        /// <returns></returns>
        public int Read(byte[] buf)
        {
            return Read(buf, 0, buf.Length);
        }

        /// <summary>
        /// Read a byte.
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return (byte)ReadBytesToValue(1);
        }

        /// <summary>
        /// Read an integer.
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            return (int)ReadBytesToValue(4);
        }

        /// <summary>
        /// Read a long.
        /// </summary>
        /// <returns>A long.</returns>
        public long ReadLong()
        {
            return ReadBytesToValue(8);
        }

        /// <summary>
        /// read a number of bytes from the stream and return them as a long.
        /// </summary>
        /// <param name="bytes">The amount of bytes to read. Max: 8.</param>
        /// <returns>A long.</returns>
        public long ReadBytesToValue(int bytes)
        {
            if (bytes > 8)
                throw new ArgumentException("Can't read more then 8 bytes to a 64 bit value.");
            byte[] buf = new byte[bytes];
            Read(buf);
            long res = 0;
            for (int i = 0; i < bytes; i++)
                res = (res << 8) + (buf[i] & 0xFF);
            return res;
        }

        /* here start the byte writing methods, there will be no conversion here
         * buffering will also take place here, including stream errors.
         */

        /// <summary>
        /// Fill the buffer buf with data from the stream, starting at given offset.
        /// blocks until the bytes are available.
        /// </summary>
        /// <param name="buf">The buff to be filled.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns>length</returns>
        protected int ReadRaw(byte[] buf, int off, int length)
        {
            long time = DateTime.Now.Ticks;
            if (stream == null)
                throw new IOException("Stream Closed, this should be given a new stream to use.");
            // start at offset
            int pos = off;
            lock (rLock)
            {
                // until length bytes are read
                while (pos < length + off)
                {
                    // starting at current location, read until the end of the buffer
                    int len = stream.Read(buf, pos, off - pos + length);
                    pos += len;
                    if (timeoutEnabled && len == 0 && DateTime.Now.Ticks - time > timeout)
                        throw new IOException("Read timed out.");
                    else time = DateTime.Now.Ticks;
                }
                Debug.Assert(pos - off == length);
            }
            return pos - off;
        }

        /// <summary>
        /// send the data from the buffer to the stream
        /// no conversion will take place.
        /// </summary>
        /// <param name="buf">The buffer to be written from.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        protected void WriteRaw(byte[] data, int off, int len)
        {
            if (stream == null)
            {
                // buffer data for later use, the stream is being rebuild now.
                Buffer(data, off, len);
            }
            else
            {
                lock (wLock)
                {
                    try
                    {
                        FlushBuffer();
                        stream.Write(data, off, len);
                    }
                    catch (IOException)
                    {
                        Open = false;
                        // buffer data for later use, the stream should be set to an active stream
                        Buffer(data, off, len);
                    }
                }
            }
        }

        /// <summary>
        /// write a newline to the stream.
        /// </summary>
        public void WriteNewline()
        {
            string msg = "\n";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
            WriteRaw(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// write the buffered data to the stream.
        /// </summary>
        protected void FlushBuffer()
        {
            if (stream != null && buffer.Count > 0)
            {
                Logger.Log(this, "Write buffered data.");
                while (buffer.Count > 0)
                {
                    stream.WriteByte(buffer.Dequeue());
                }
                Logger.Log(this, "Written all Buffered data.");
            }
        }

        /// <summary>
        /// Underlying stream.
        /// </summary>
        public Stream InternalStream {
            get { return stream; }
            set { 
                stream = value; 
                InitStream();
            }
        }

        /// <summary>
        /// boolean indicationg if this stream is open for data input/output
        /// </summary>
        public bool Open
        {
            get { return stream != null; }
            set { if (!value) stream = null; }
        }

        /// <summary>
        /// Closes the stream.
        /// </summary>
        public void Close()
        {
            buffer.Clear();
            if (stream != null)
            {
                stream.Close();
            }
        }

        /// <summary>
        /// Write data to the stream, starting from given offset, writing len bytes.
        /// </summary>
        /// <param name="data">Data to be written to the socket.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to be written.</param>
        public abstract void Write(byte[] data, int off, int len);

        /// <summary>
        /// Fill the buffer with data from the stream, starting at given offset.
        /// </summary>
        /// <param name="buf">The buff to be filled.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns></returns>
        public abstract int Read(byte[] buf, int off, int length);

        /// <summary>
        /// indicates wether or not this class supports string reading.
        /// </summary>
        public abstract bool CanReadString
        {
            get;
        }

        /// <summary>
        /// indicates wether or not this class supports string writing.
        /// </summary>
        public abstract bool CanWriteString
        {
            get;
        }

        /// <summary>
        /// Write a string to the stream.
        /// </summary>
        /// <param name="x">A string.</param>
        public abstract void WriteString(string x);

        /// <summary>
        /// Read a string.
        /// </summary>
        /// <returns>A string.</returns>
        public abstract string ReadString();

        /// <summary>
        /// binary string representation of a given long.
        /// </summary>
        /// <param name="x">A long.</param>
        /// <param name="bits">A number of bits.</param>
        /// <returns>A binary string of bits length.</returns>
        protected static String ToBitString(long x, int bits)
        {
            String res = "";
            for (int i = 0; i < bits; i++)
            {
                // bit by bit, if it is 1, prepend a 1, else prepend a 0
                if (x % 2 == 0) res = "0" + res;
                else res = "1" + res;
                x = (x >> 1);
            }
            return res;
        }
    }
}
