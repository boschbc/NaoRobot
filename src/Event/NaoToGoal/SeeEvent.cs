using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Nao is at object ID
     */
    public class SeeEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.See;
        /*
         * Explicit constructor.
         */
        public SeeEvent(int objectID, int distance) : base((byte) EventCode.See, objectID, distance) { }

    }
}
