
namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Sends information regarding a visible object to Goal.
    /// </summary>
    public sealed class SeeEvent : DataSendingNaoEvent
    {
        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="objectID">The ID of the spotted object.</param>
        public SeeEvent(int objectID) : base((byte) EventCode.See, objectID) { }

    }
}
