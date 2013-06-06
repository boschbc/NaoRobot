using System;
using System.Collections.Generic;
using System.Drawing;

using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Sends the position the Nao is at to Goal.
     */
    /// <summary>
    /// Sends the position the Nao is at to Goal.
    /// </summary>
    public class LocationEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Location;

        /*
         * Constructor.
         */
        public LocationEvent(int roomID) : base((byte) EventCode.Location, roomID) { }

        public LocationEvent(int x, int y) : base((byte)EventCode.Location, NaoState.Instance.Map.TileAt(x, y).ID) { }

    }
}
