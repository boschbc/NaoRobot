using System;
using System.Collections.Generic;

using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    /*
     * Stop all actions the Nao is doing.
     */
    public class HaltEvent : NaoEvent
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
