using System;

using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// The exit events signals that the program should be terminated.
    /// </summary>
    public sealed class ExitEvent : NaoEvent
    {
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
    }
}
