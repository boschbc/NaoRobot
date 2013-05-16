using System;
using System.Runtime.Serialization;

namespace Naovigate.Event
{
    /*
     * An exception designed to be thrown when trying to initialise
     * a NaoEvent with an invalid byte action-code.
     */
    [Serializable]
    public class InvalidEventCodeException : Exception
    {
        public InvalidEventCodeException() : base() { }
        public InvalidEventCodeException(string message) : base(message) { }

        //Allows serialization of this exception.
        protected InvalidEventCodeException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }
    }
}
