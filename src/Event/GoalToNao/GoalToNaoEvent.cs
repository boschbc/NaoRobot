using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    public abstract class GoalToNaoEvent : NaoEvent
    {
        /*
         * default constructor used for extending wihtout using a stream
         */
        public GoalToNaoEvent()
        {
            Unpack();
        }

        /**
        * See the INaoEvent class docs for documentation of this method.
        **/
        public override abstract void Fire();

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public override abstract void Abort();

        /**
        * Uses the CommunicationStream to extracts different parameters as required.
        **/
        protected abstract void Unpack();        
    }
}
