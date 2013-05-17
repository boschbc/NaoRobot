using System;

using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    /*
     * The exit events signals that the program should be terminated.
     */
    public class ExitEvent : NaoEvent
    {
        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {

        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Abort()
        {

        }
    }
}
