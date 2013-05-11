using System;
using System.Collections.Generic;

using Naovigate.Communication;

namespace Naovigate.Event
{
    /**
     * Creates new Nao-events on-demand.
     **/
    public class NaoEventFactory
    {
        private static string InvalidActionCodeMsg = "Attempting to construct a NaoEvent with a corrupt action-code.";

        public enum ActionCode
        {
            Move = 0,
        }
        private static Dictionary<byte, Func<NaoEvent>> CodeConverter = 
            new Dictionary<byte, Func<NaoEvent>>()
            {
                {(byte) ActionCode.Move, delegate() {return new MoveNaoEvent();}},
            };

        /**
         * Creates and returns a new Nao-event.
         * @param acb: A byte representing the action-code.
         * @param stream: A communication stream representing additional parameters specific to the given action-code event type.
         * @throws InvalidActionCodeException if the given action code byte is not recognised.
         **/
        public static INaoEvent NewEvent(byte acb)
        {
           if (!CodeConverter.ContainsKey(acb))
                throw new InvalidActionCodeException(InvalidActionCodeMsg);
           return CodeConverter[acb]();
        }
    }
}
