using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Naovigate.Util;
namespace Naovigate.Communication
{
    /// <summary>
    /// A wrapper class for a System.IO.Stream.
    /// </summary>
    public class CommunicationStream : AbstractCommunicationStream
    {
        /// <summary>
        /// Creates a new instance wrapped around a given stream.
        /// </summary>
        /// <param name="stream">The stream to wrap.</param>
        public CommunicationStream(Stream stream) : base(stream) {}

        /// <summary>
        /// Write from data to the stream, starting from given offset, writing len bytes.
        /// </summary>
        /// <param name="data">Data to be written to the socket.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to be written.</param>
        public override void Write(byte[] data, int off, int len)
        {
            WriteRaw(data, off, len);
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
            return ReadRaw(buf, off, length);
        }

        public override string ReadString()
        {
            throw new NotSupportedException();
        }

        public override void WriteString(string x)
        {
            throw new NotSupportedException();
        }

        public override bool CanReadString
        {
            get { return false; }
        }

        public override bool CanWriteString
        {
            get { return false; }
        }
    }
}