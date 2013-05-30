using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
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
        /// write the buffered data to the stream.
        /// </summary>
        protected override void FlushBuffer()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write from data to the stream, starting from given offset, writing len bytes.
        /// </summary>
        /// <param name="data">Data to be written to the socket.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to be written.</param>
        public override void Write(byte[] data, int off, int len)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fill the buffer buf with data from the stream, starting at off blocks until the bytes are available
        /// </summary>
        /// <param name="buf">The buff to be filled.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns></returns>
        public override int Read(byte[] buf, int off, int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Write a string to the stream.
        /// </summary>
        /// <param name="x">A string.</param>
        public override void WriteString(string x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read a string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ReadString()
        {
            throw new NotImplementedException();
        }
    }
}
