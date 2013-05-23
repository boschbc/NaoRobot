using System;

using Naovigate.Util;
using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    /*
     * The exit events signals that the program should be terminated.
     */
    public class ExitEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Exit;
        private static readonly String ExitMessage = "Never gonna give you up. Shutting down.";
        
        public ExitEvent() : base(Priority.High) { }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            NaoState.Instance.SpeechProxy.say(ExitMessage);
            Environment.Exit(0);
        }

        /*
         * Exit can not be aborted.
         */
        public override void Abort() { }
    }
}
