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
        /*
         * Implicit constructor.
         */
        public LocationEvent(int roomID) : base((byte) EventCode.Location, roomID) { }

    }
}
