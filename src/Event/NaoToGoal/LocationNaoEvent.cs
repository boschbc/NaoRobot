using System;
using Naovigate.Communication;

namespace Naovigate.Event.NaoToGoal
{
    /**
     * Send the position the Nao is at.
     */
    public class LocationNaoEvent : DataSendingNaoEvent
    {
        private static readonly byte eventID = 0x89;

        /**
        * create a new LocationNaoEvent, with the specified locationID to be sent
        */
        public LocationNaoEvent(int locationID) : base(eventID, locationID) {}

        /**
         * Fires the event.
         **/
        public override void Fire()
        {
            SendAsInt();
        }
    }
}
