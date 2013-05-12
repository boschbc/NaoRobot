using System;
using System.Collections.Generic;

using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    /*
     * Describes a NaoEvent from the Goal client to the Nao client.
     */
    public abstract class GoalToNaoEvent : NaoEvent
    {
        /*
         * Default constructor.
         */
        public GoalToNaoEvent() { }

        /*
         * A constructor which extracts this event's parameters from a stream.
         */
        public GoalToNaoEvent(CommunicationStream stream)
        {
            Read(stream);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override abstract void Fire();

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override abstract void Abort();

        /*
         * Takes a communication stream and extracts different parameters as required.
         * All subclasses should provide an implementation of this method.
         */
        protected virtual void Read(CommunicationStream stream) { }
    }
}
