using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace Naovigate.Communication
{
    class CommunicationStream
    {
        private Stream stream;
        public CommunicationStream(Stream stream)
        {
            this.stream = stream;
        }

        // write to the socket.
        public void Write(byte[] data)
        {
            Write(data, 0, data.Length);
        }

        /**
         * write from data to the socket, starting from off, writing len bytes
         */
        public void Write(byte[] data, int off, int len)
        {
            Console.WriteLine("GoalCommunicator.Write " + len + " Bytes");
            stream.Write(data, off, len);
        }

        /**
         * write an integer to the socket
         */
        public void WriteInt(int x)
        {
            WriteBytesFromValue(x, 4);
        }
        /**
         * write a long to the socket
         */
        public void WriteLong(long x)
        {
            WriteBytesFromValue(x, 8);
        }

        /**
         * write the last "bytes" bytes from x
         */
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

        // read from the socket.
        /**
         * fill the buffer buf with data from the socket
         */
        public int Read(byte[] buf)
        {
            return Read(buf, 0, buf.Length);
        }

        /**
         * fill the buffer buf with data from the socket, starting at off
         * blocks until the bytes are available
         */
        public int Read(byte[] buf, int off, int length)
        {
            Console.WriteLine("GoalCommunicator.Read " + length + " Bytes");
            // start at offset
            int pos = off;
            // until length bytes are read
            while (pos < length + off)
            {
                // starting at current location, read until the end of the buffer
                int len = stream.Read(buf, pos, off - pos + length);
                pos += len;
            }
            Debug.Assert(pos - off == length);
            return pos - off;
        }

        /**
         * read an integer
         */
        public int ReadInt()
        {
            return (int)ReadBytesToValue(4);
        }
        /**
         * read a long
         */
        public long ReadLong()
        {
            return ReadBytesToValue(8);
        }
        /**
         * read a number of bytes from the socket and return them as a long
         * read maximum of 8 bytes
         */
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

        /**
         * return the underlying stream
         */
        public Stream GetStream()
        {
            return stream;
        }

        //debugging help function
        public static String ToBitString(long x)
        {
            String res = "";
            for (int i = 0; i < 64; i++)
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
