using System.Collections.Generic;
using System.IO;
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
            Logger.Log("BSComs", "Write " + s);
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            WriteRaw(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Fill the buffer buf with data from the stream, starting at off.
        /// Blocks until the bytes are available.
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

        /// <summary>
        /// read a byte from the stream
        /// </summary>
        /// <returns></returns>
        private byte ReadByteFromBitString()
        {
            string bitstring = GetNextBitString();
            int value = 0;
            for (int i = 7; i >= 0;i-- )
            {
                if (bitstring[i] == '1')
                    value += 1 << (7 - i);
            }
            Logger.Log("BSComs", "read " + value + "(" + bitstring + ")");
            return (byte) value;
        }

        /// <summary>
        /// read a string of length 8 from the stream.
        /// </summary>
        /// <returns></returns>
        private string GetNextBitString()
        {
            byte[] str = new byte[8];
            for (int i = 0; i < str.Length; i++)
            {
                str[i] = ReadNextByte();
            }
            return Encoding.UTF8.GetString(str);
        }

        /// <summary>
        /// Return the next byte of the stream, ignoring newlines.
        /// </summary>
        /// <returns>The next byte of the stream, ignoring newlines.</returns>
        private byte ReadNextByte()
        {
            return ReadNextByte(true);
        }

        /// <summary>
        /// Return the next byte of the stream.
        /// </summary>
        /// <param name="ignoreNewline"></param>
        /// <returns>The next byte of the stream.</returns>
        private byte ReadNextByte(bool ignoreNewline)
        {
            byte[] buf = new byte[1];
            byte b;
            do
            {
                ReadRaw(buf, 0, buf.Length);
                b = buf[0];
                // ignore newlines
            } while (ignoreNewline && b == '\n');
            return b;
        }

        /// <summary>
        /// Write a string to the stream.
        /// </summary>
        /// <param name="x">A string.</param>
        public override void WriteString(string msg)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(msg);
            WriteRaw(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Read a string, stopping at the first newline.
        /// consumes the newline
        /// </summary>
        /// <returns>A string. Consumed newlines.</returns>
        public override string ReadString()
        {
            List<byte> value = new List<byte>();

            byte b;
            while ((b = ReadNextByte(false)) != '\n') {
                value.Add(b);
            }

            return Encoding.UTF8.GetString(value.ToArray());
        }

        public override bool CanReadString
        {
            get { return true; }
        }

        public override bool CanWriteString
        {
            get { return true; }
        }
    }
}
