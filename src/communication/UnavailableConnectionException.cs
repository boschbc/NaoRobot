using System;
using System.Net;
using System.Runtime.Serialization;

namespace Naovigate.Communication
{
    /// <summary>
    /// An exception designed to be thrown when the connection to a ALProxy becomes unavailable.
    /// </summary>
    [Serializable]
    public class UnavailableConnectionException : Exception
    {
        private static string msgIPFormat = "{0}\n\tIP: {1}";
        private static string msgIPPortFormat = msgIPFormat + "\n\tPort: {2}";

        /// <summary>
        /// Builds an error message including IP address.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="ip">A string containing an IP.</param>
        /// <returns>A formatted string including the given parameters.</returns>
        private static string buildIPError(string message, string ip)
        {
            return String.Format(msgIPFormat, message, ip);
        }

        /// <summary>
        /// Builds an error message including IP address and port number.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="ip">A string containing an IP.</param>
        /// <param name="port">A port number.</param>
        /// <returns>A formatted string including the given parameters.</returns>
        private static string buildIPError(string message, string ip, int port)
        {
            return String.Format(msgIPPortFormat, message, ip, port);
        }

        public UnavailableConnectionException() : base() { }

        public UnavailableConnectionException(string message) : base(message) { }

        public UnavailableConnectionException(string message, string ip) : 
            base(buildIPError(message, ip)) { }

        public UnavailableConnectionException(string message, IPAddress ip) :
            base(buildIPError(message, ip.ToString())) { }

        public UnavailableConnectionException(string message, IPEndPoint endpoint) :
            base(buildIPError(message, endpoint.ToString())) { }

        public UnavailableConnectionException(string message, string ip, int port) : 
            base(buildIPError(message, ip, port)) { }

        public UnavailableConnectionException(string message, IPAddress ip, int port) :
            base(buildIPError(message, ip.ToString(), port)) { }

        public UnavailableConnectionException(string message, IPEndPoint endpoint, int port) :
            base(buildIPError(message, endpoint.ToString(), port)) { }


        /// <summary>
        /// Allows serialization of this exception.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="ctxt"></param>
        protected UnavailableConnectionException(SerializationInfo info, StreamingContext ctxt) : 
            base(info, ctxt) { }
    }
}
