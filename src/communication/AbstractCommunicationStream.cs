﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
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
        private static readonly bool timeoutEnabled = false;
        private static readonly int ticksToMsRatio = 10000;
        protected static readonly int timeoutInMs = 10000;
        protected static readonly int timeout = timeoutInMs * ticksToMsRatio;
        protected Queue<byte> buffer;
        protected Stream stream;
        protected object wLock = new object();
        protected object rLock = new object();

        /// <summary>
        /// Creates a new instance wrapped around a given stream.
        /// </summary>
        /// <param name="stream">The stream to wrap.</param>
        public AbstractCommunicationStream(Stream stream)
        {
            this.stream = stream;
            InitStream();
            buffer = new Queue<byte>();
        }

        /// <summary>
        /// set the timeout of this stream, if this stream supports timeouts
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
        /// buffer the data 
        /// </summary>
        /// <param name="data">the data</param>
        /// <param name="off">point to start writing from</param>
        /// <param name="len">the amount of bytes to buffer</param>
        protected void Buffer(byte[] data, int off, int len)
        {
            Logger.Log(this, "Stream null, Buffering data.");
            for (int i = off; i < off + len; i++)
            {
                buffer.Enqueue(data[i]);
            }
        }

        

        /// <summary>
        /// Write to the stream.
        /// </summary>
        /// <param name="data"></param>
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
        /// Writes the last bytes from x.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="bytes"></param>
        public void WriteBytesFromValue(long x, int bytes)
        {
            byte[] data = new byte[bytes];
            for (int i = bytes - 1; i >= 0; i--)
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
        ///  read a number of bytes from the stream and return them as a long.
        /// </summary>
        /// <param name="bytes">The amount of bytes to read. Max: 8.</param>
        /// <returns>A long.</returns>
        public long ReadBytesToValue(int bytes)
        {
            if (bytes > 8) throw new ArgumentException("Can't read more then 8 bytes to a 64 bit value.");
            byte[] buf = new byte[bytes];
            Read(buf);
            long res = 0;
            for (int i = 0; i < bytes; i++)
            {
                res = (res << 8);
                res += (buf[i] & 0xFF);
            }
            return res;
        }

        /* here start the byte writing methods, there will be no conversion here
         * buffering will also take place here, including stream errors.
         */

        /// <summary>
        /// Fill the buffer buf with data from the stream, starting at off.
        /// blocks until the bytes are available.
        /// </summary>
        /// <param name="buf">The buff to be filled.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns>length</returns>
        protected int ReadBytesBlocking(byte[] buf, int off, int length)
        {
            long time = DateTime.Now.Ticks;
            if (stream == null) throw new IOException("Stream Closed, this should be given a new stream to use.");
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
                    if (timeoutEnabled && len == 0)
                    {
                        if ((DateTime.Now.Ticks - time) > timeout)
                            throw new IOException("Read timed out.");

                    }
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
        protected void WriteBytes(byte[] data, int off, int len)
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

        public void WriteNewline()
        {
            string msg = "\n";
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
            WriteBytes(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// write the buffered data to the stream.
        /// </summary>
        protected void FlushBuffer()
        {
            if (stream != null)
            {
                if (buffer.Count > 0)
                {
                    Logger.Log(this, "Write buffered data.");
                    while (buffer.Count > 0)
                    {
                        stream.WriteByte(buffer.Dequeue());
                    }
                    Logger.Log(this, "Written all Buffered data.");
                }
            }
        }

        /// <summary>
        /// Underlying stream.
        /// </summary>
        public Stream Stream {
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
            set { if(!value) stream = null; }
        }

        /// <summary>
        /// Closes the stream.
        /// </summary>
        public void Close()
        {
            buffer.Clear();
            if(stream != null) stream.Close();
        }

        /// <summary>
        /// Write from data to the stream, starting from given offset, writing len bytes.
        /// </summary>
        /// <param name="data">Data to be written to the socket.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to be written.</param>
        public abstract void Write(byte[] data, int off, int len);

        /// <summary>
        /// Fill the buffer buf with data from the stream, starting at off.
        /// </summary>
        /// <param name="buf">The buff to be filled.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns></returns>
        public abstract int Read(byte[] buf, int off, int length);

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
        /// <returns>A binary readable string.</returns>
        public static String ToBitString(long x)
        {
            return ToBitString(x, 64);
        }

        /// <summary>
        /// binary string representation of a given long.
        /// </summary>
        /// <param name="x">A long.</param>
        /// <returns>A binary readable string of bits length.</returns>
        public static String ToBitString(long x, int bits)
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
