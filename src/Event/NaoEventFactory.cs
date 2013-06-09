using System;
using System.Collections.Generic;

using Naovigate.Communication;
using Naovigate.Event.GoalToNao;
using Naovigate.Event.NaoToGoal;

namespace Naovigate.Event
{
    /// <summary>
    /// Creates new NaoEvents using their byte-code as defined in the Goal-Nao API.
    /// </summary>
    public static class NaoEventFactory
    {
        private static string InvalidActionCodeMsg = "Attempting to construct a NaoEvent with a corrupt action-code.";
        private static Dictionary<byte, Func<NaoEvent>> CodeConverter = 
            new Dictionary<byte, Func<NaoEvent>>()
            {
                {(byte) EventCode.Exit, () => new ExitEvent() },
                {(byte) EventCode.GoTo, () => new GoToEvent() },
                {(byte) EventCode.Halt, () => new HaltEvent() },
                {(byte) EventCode.Pickup, () => new PickupEvent() },
                {(byte) EventCode.PutDown, () => new PutDownEvent() },
            };

        /// <summary>
        /// Creates a new Nao event best on the given byte-code.
        /// </summary>
        /// <param name="code">A byte-code of an event as defined in the Goal-Nao API.</param>
        /// <returns>A NaoEvent instance.</returns>
        /// <exception cref="InvalidEventCodeException">The byte-code supplied does not match any event-definition in the API.</exception>
        public static INaoEvent NewEvent(byte code)
        {
           if (!CodeConverter.ContainsKey(code))
                throw new InvalidEventCodeException(InvalidActionCodeMsg);
           return CodeConverter[code]();
        }
    }
}
