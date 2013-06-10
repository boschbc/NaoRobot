using System.IO;

namespace Naovigate.Communication
{
    /// <summary>
    /// interface for translating commonly used data (int, long, etc) from and to a Stream.
    /// </summary>
    public interface ICommunicationStream
    {
        /// <summary>
        /// Write to the stream.
        /// </summary>
        /// <param name="data">The data.</param>
        void Write(byte[] data);
        
        /// <summary>
        /// Write from data to the stream, starting from given offset, writing len bytes.
        /// </summary>
        /// <param name="data">Data to be written to the stream.</param>
        /// <param name="off">Offset.</param>
        /// <param name="len">The amount of bytes to be written.</param>
        void Write(byte[] data, int off, int len);
        
        /// <summary>
        /// Write a byte to the stream.
        /// </summary>
        /// <param name="x">A byte.</param>
        void WriteByte(byte x);
        
        /// <summary>
        /// Write an integer to the stream.
        /// </summary>
        /// <param name="x">An integer.</param>
        void WriteInt(int x);

        /// <summary>
        /// Write a long to the stream.
        /// </summary>
        /// <param name="x">A long.</param>
        void WriteLong(long x);

        /// <summary>
        /// indicates wether or not this class supports string writing.
        /// </summary>
        bool CanWriteString
        {
            get;
        }

        /// <summary>
        /// Write a string to the stream.
        /// This is an optional method, indicated by CanWriteString
        /// </summary>
        /// <param name="x">A string.</param>
        void WriteString(string x);

        /// <summary>
        /// write a newline to the stream.
        /// </summary>
        void WriteNewline();

        /// <summary>
        /// Fill the buffer buf with data from the stream.
        /// </summary>
        /// <param name="buf">The buffer to be filled.</param>
        /// <returns></returns>
        int Read(byte[] buf);

        /// <summary>
        /// Fill the buffer buf with data from the stream, starting at off.
        /// </summary>
        /// <param name="buf">The buff to be filled.</param>
        /// <param name="off">Offset.</param>
        /// <param name="length">The amount of bytes to read.</param>
        /// <returns></returns>
        int Read(byte[] buf, int off, int length);

        /// <summary>
        /// Read a byte.
        /// </summary>
        /// <returns></returns>
        byte ReadByte();

        /// <summary>
        /// Read an integer.
        /// </summary>
        /// <returns></returns>
        int ReadInt();

        /// <summary>
        /// Read a long.
        /// </summary>
        /// <returns>A long.</returns>
        long ReadLong();

        /// <summary>
        /// indicates wether or not this class supports string reading.
        /// </summary>
        bool CanReadString
        {
            get;
        }

        /// <summary>
        /// Read a string until the next newline.
        /// Consumes the newline.
        /// This is an optional method, indicated by CanReadString
        /// </summary>
        /// <returns>A string.</returns>
        string ReadString();

        /// <summary>
        /// Underlying stream.
        /// </summary>
        Stream Stream
        {
            get;
            set;
        }

        /// <summary>
        /// boolean indicationg if this stream is open for data input/output
        /// </summary>
        bool Open
        {
            get;
            set;
        }

        /// <summary>
        /// Closes the stream.
        /// </summary>
        void Close();
    }
}
