
namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Notifies the state of the Nao.
    /// Either 1 - walking or 2 - halted.
    /// </summary>
    public sealed class StateEvent : DataSendingNaoEvent
    {
        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="state">An integer representing the state the Nao is in as defined by the Goal-Nao API.</param>
        public StateEvent(int state) : base((byte) EventCode.State, state) { }
    }
}
