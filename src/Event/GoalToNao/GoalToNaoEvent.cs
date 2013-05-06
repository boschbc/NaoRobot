using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    public abstract class GoalToNaoEvent : NaoEvent
    {
        private CommunicationStream stream;

        /*
         * default constructor used for extending wihtout using a stream
         */
        public GoalToNaoEvent()
        {

        }

        /*
         * 
         */
        public GoalToNaoEvent(CommunicationStream stream)
        {
            this.stream = stream;
            Unpack(stream);
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
        * Takes a communication stream and extracts different parameters as required.
        * All subclasses should provide an implementation of this method.
        **/
        protected abstract void Unpack(CommunicationStream stream);        
    }
}
