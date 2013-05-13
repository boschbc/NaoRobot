using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /**
     * Notify Goal that the Nao is holding object ID.
     */
    public class HoldingEvent : DataSendingNaoEvent
    {
        /**
        * create a new HoldingNaoEvent, with the specified id of the object the Nao is holding.
        */
        public HoldingEvent(int objectID) : base((byte) EventCode.Holding, objectID) { }

    }
}
