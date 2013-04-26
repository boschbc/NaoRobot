using System;
using System.Runtime.Serialization;

namespace Naovigate.Communication
{
    /**
     * An exception designed to be thrown when trying to initialise
     * a NaoEvent with an invalid byte action-code.
     **/
    [Serializable]
    public class InvalidActionCodeException : Exception
    {
        public InvalidActionCodeException() : base() { }
        public InvalidActionCodeException(string message) : base(message) { }

        //Allows serialization of this exception.
        protected InvalidActionCodeException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt) { }
    }
}
