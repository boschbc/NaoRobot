using System;
using System.Collections.Generic;
using Naovigate.Communication;

namespace Naovigate.Event
{
    /**
     * Creates new Nao-events on-demand.
     **/
    class NaoEventFactory
    {
        private static string InvalidActionCodeMsg = "Attempting to construct a NaoEvent with a corrupt action-code.";

        private static Dictionary<byte, Func<CommunicationStream, NaoEvent>> CodeConverter = 
            new Dictionary<byte, Func<CommunicationStream, NaoEvent>>()
            {
                {(byte) 0, stream => new MoveNaoEvent(stream)},
                {(byte) 1, stream => new LookNaoEvent(stream)},
                {(byte) 2, stream => new GrabNaoEvent(stream)}
            };

        /**
         * Creates and returns a new Nao-event.
         * @param acb: A byte representing the action-code.
         * @param stream: A communication stream representing additional parameters specific to the given action-code event type.
         * @throws InvalidActionCodeException if the given action code byte is not recognised.
         **/
        public static INaoEvent NewEvent(byte acb, CommunicationStream stream)
        {
           if (!CodeConverter.ContainsKey(acb))
                throw new InvalidActionCodeException(InvalidActionCodeMsg);
           return CodeConverter[acb](stream);
        }
    }
}
