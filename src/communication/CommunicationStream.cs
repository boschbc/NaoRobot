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
        /// write the buffered data to the stream.
        /// </summary>
        protected override void FlushBuffer()
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
        /// Write from data to the stream, starting from given offset, writing len bytes.
        /// </summary>
        /// <param name="data">Data to be written to the socket.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to be written.</param>
        public override void Write(byte[] data, int off, int len)
        {
            if (stream == null)
            {
                // cache data for later use, the stream is being rebuild now.
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
                    } catch(IOException){
                        Open = false;
                        // cache data for later use, the stream should be set to an active stream
                        Buffer(data, off, len);
                    }
                }
            }
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
            return ReadBytesBlocking(buf, off, length);
        }

        public override string ReadString()
        {
            throw new NotImplementedException();
        }

        public override void WriteString(string x)
        {
            throw new NotImplementedException();
        }
    }
}