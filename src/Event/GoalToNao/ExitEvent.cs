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
        
        public ExitEvent() : base(Priority.High, ExecutionBehavior.Instantaneous) { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            if(NaoState.Instance.Connected)
                Proxies.GetProxy<Aldebaran.Proxies.TextToSpeechProxy>().say(ExitMessage);
            Environment.Exit(0);
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Exit; }
        }

        /// <summary>
        /// Abort's this event's execution.
        /// The exit-event is not durative and therefore cannot be aborted (has no effect).
        /// </summary>
        public override void Abort() { }
    }
}
