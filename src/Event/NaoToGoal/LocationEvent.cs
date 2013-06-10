
using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Sends the position of the Nao to goal.
    /// </summary>
    /// <summary>
    /// Sends the position the Nao is at to Goal.
    /// </summary>
    public sealed class LocationEvent : DataSendingNaoEvent
    {
        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="roomID">The room the Nao is in.</param>
        public LocationEvent(int roomID) : base((byte) EventCode.Location, roomID) { }

        public LocationEvent(int x, int y) : base((byte)EventCode.Location, NaoState.Instance.Map.TileAt(x, y).ID) { }

    }
}
