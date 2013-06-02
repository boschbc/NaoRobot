using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Text;
using Naovigate.Util;
namespace Naovigate.Communication
{
    public class BitStringCommunicationStream : AbstractCommunicationStream
    {
        /// <summary>
        /// Creates a new instance wrapped around a given stream.
        /// </summary>
        /// <param name="stream">The stream to wrap.</param>
        public BitStringCommunicationStream(Stream stream) : base(stream) { }

        /// <summary>
        /// Write from data to the stream, starting from given offset, writing len bytes.
        /// </summary>
        /// <param name="data">Data to be written to the socket.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to be written.</param>
        public override void Write(byte[] buf, int off, int len)
        {
            for (int i = off; i < off + len;i++ )
            {
                WriteByteAsBitString(buf[i]);
            }
        }

        private void WriteByteAsBitString(byte b)
        {
            string s = ToBitString(b, 8);
            Logger.Log(this, "Write " + s);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            WriteRaw(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Fill the buffer buf with data from the stream, starting at off blocks until the bytes are available
        /// </summary>
        /// <param name="buf">The buff to be filled.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns></returns>
        public override int Read(byte[] buf, int off, int len)
        {
            for (int i = off; i < off + len; i++)
            {
                buf[i] = ReadByteFromBitString();
            }
            return len;
        }

        private byte ReadByteFromBitString()
        {
            byte[] str = new byte[8];
            for (int i = 0; i < str.Length;i++ )
            {
                str[i] = ReadNextByte();
            }
            string bitstring = Encoding.UTF8.GetString(str);
            Logger.Log(this, "read "+bitstring);
            int value = 0;
            for (int i = str.Length - 1; i >= 0;i-- )
            {
                value = value + (bitstring[i] == '1' ? 1 << (str.Length - i - 1) : 0);
            }
            return (byte) value;
        }

        private byte ReadNextByte()
        {
            byte[] buf = new byte[1];
            byte b;
            do
            {
                ReadRaw(buf, 0, buf.Length);
                b = buf[0];
                // ignore newlines
            } while(b == '\n');
            return b;
        }

        /// <summary>
        /// Write a string to the stream.
        /// </summary>
        /// <param name="x">A string.</param>
        public override void WriteString(string msg)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
            WriteRaw(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Read a string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ReadString()
        {
            List<byte> value = new List<byte>();

            byte b;
            while ((b = this.ReadNextByte()) != 0) {
                value.Add(b);
            }
            value.Add(0);

            return Encoding.UTF8.GetString(value.ToArray());
        }
    }
}
