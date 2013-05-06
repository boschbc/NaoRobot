using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    /*
     * Stop all actions the nao is doing.
     */
    public class StopNaoEvent : GoalToNaoEvent
    {
        public StopNaoEvent(CommunicationStream stream) : base(stream)
        {

        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public override void Fire()
        {

        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public override void Abort()
        {

        }

        /**
        * Takes a communication stream and extracts different parameters as required.
        **/
        protected override void Unpack(CommunicationStream stream)
        {

        }
    }
}
