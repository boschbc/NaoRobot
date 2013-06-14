
namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Notifies the state of the Nao.
    /// Either 1 - walking or 0 - halted.
    /// </summary>
    public sealed class StateEvent : DataSendingNaoEvent
    {
        private int state;

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="state">An integer representing the state the Nao is in as defined by the Goal-Nao API.</param>
        public StateEvent(int state) : base((byte)EventCode.State, state) { this.state = state; }

        public override string ToString()
        {
            return base.ToString() + "<" + state + ">";
        }
    }
}
