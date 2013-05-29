using System;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// The exit events signals that the program should be terminated.
    /// </summary>
    public class ExitEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Exit;
        private static readonly String ExitMessage = "Never gonna give you up. Shutting down.";
        
        public ExitEvent() : base(Priority.High) { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            NaoState.Instance.SpeechProxy.say(ExitMessage);
            Environment.Exit(0);
        }

        /// <summary>
        /// Abort's this event's execution.
        /// The exit-event is not durative and therefore cannot be aborted (has no effect).
        /// </summary>
        public override void Abort() { }
    }
}
