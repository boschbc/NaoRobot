using System;
using System.Collections.Generic;
using System.Drawing;

using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Sends the position of the Nao to goal.
    /// </summary>
    public class LocationEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Location;

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="roomID">The room the Nao is in.</param>
        public LocationEvent(int roomID) : base((byte) EventCode.Location, roomID) { }

    }
}
