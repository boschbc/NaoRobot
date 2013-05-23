using System;
using System.Collections.Generic;
using System.Drawing;

using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Sends the position the Nao is at to Goal.
     */
    public class LocationEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Location;

        /*
         * Implicit constructor.
         */
        public LocationEvent(int roomID) : base((byte) EventCode.Location, roomID) { }

    }
}
