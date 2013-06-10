
namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Notifies Goal that the Nao is holding an object.
    /// </summary>
    public sealed class HoldingEvent : DataSendingNaoEvent
    {
        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="objectID">The ID of the object the Nao is holding.</param>
        public HoldingEvent(int objectID) : base((byte) EventCode.Holding, objectID) { }

    }
}
