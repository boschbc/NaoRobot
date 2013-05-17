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
        private static readonly String ExitMessage = "Never gonna give you up. Shutting down.";
        
        public ExitEvent() : base(Priority.High) { }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            NaoState.SpeechProxy.say(ExitMessage);
            Environment.Exit(0);
        }

        /*
         * Exit can not be aborted.
         */
        public override void Abort() { }
    }
}
