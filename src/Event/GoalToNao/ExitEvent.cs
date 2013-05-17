using System;
using System.Collections.Generic;
using Naovigate.Util;
using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    /*
     * The exit events signals that the program should be terminated.
     */
    public class ExitEvent : NaoEvent
    {
        private static readonly String exitMessage = "Never Gona Give You Up. Shutting down";
        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public ExitEvent()
        {
            this.Priority = Priority.High;
        }

        public override void Fire()
        {
            NaoState.SpeechProxy.say(exitMessage);
            Environment.Exit(0);
        }

        /*
         * Exit can not be aborted.
         */
        public override void Abort() { }
    }
}
