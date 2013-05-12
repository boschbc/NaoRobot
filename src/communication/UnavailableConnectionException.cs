using System;
using System.Runtime.Serialization;

namespace Naovigate.Communication
{
    /*
    * An exception designed to be thrown when the connection to a ALProxy becomes unavailable.
    */
    [Serializable]
    public class UnavailableConnectionException : Exception
    {
        private static string msgIPFormat = "{0}\n\tIP: {1}";
        private static string msgIPPortFormat = msgIPFormat + "\n\tPort: {2}";

        /*
         * Builds an error message including IP address.
         */
        private static string buildIPError(string message, string ip)
        {
            return String.Format(msgIPFormat, message, ip);
        }

        /*
         * Builds an error message including IP address and port number.
         */
        private static string buildIPError(string message, string ip, int port)
        {
            return String.Format(msgIPPortFormat, message, ip, port);
        }

        /*
         * Exceptions
         */
        public UnavailableConnectionException() : base() { }
        public UnavailableConnectionException(string message) : base(message) { }
        public UnavailableConnectionException(string message, string ip) : 
            base(buildIPError(message, ip)) { }
        public UnavailableConnectionException(string message, string ip, int port) : 
            base(buildIPError(message, ip, port)) { }


        /*
         * Allows serialization of this exception.
         */
        protected UnavailableConnectionException(SerializationInfo info, StreamingContext ctxt) : 
            base(info, ctxt) { }
    }
}
