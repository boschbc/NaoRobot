using System;
using Naovigate.Communication;

namespace Naovigate.Event.NaoToGoal
{
    /**
     * Notify Goal that the Nao is holding object ID.
     */
    public class HoldingNaoEvent : DataSendingNaoEvent
    {
        private static readonly byte eventID = 0x8d;

        /**
        * create a new HoldingNaoEvent, with the specified id of the object the Nao is holding.
        */
        public HoldingNaoEvent(int objectID) : base(eventID, objectID) {}

        /**
         * Fires the event.
         **/
        public override void Fire()
        {
            SendAsInt();
        }
    }
}
