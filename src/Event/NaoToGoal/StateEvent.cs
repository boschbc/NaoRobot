
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
        public StateEvent(int state) : base((byte)EventCode.State, state) { 
            this.state = state;
            for (int i = 0; i < 10;i++ )
            {
                Naovigate.Util.Logger.Log(this, "STATE CHANGED");
            }
        }

        public override void Fire()
        {
            base.Fire();
            for (int i = 0; i < 10; i++)
            {
                Naovigate.Util.Logger.Log(this, "STATE CHANGE FIRED: "+state);
            }
        }

        public override string ToString()
        {
            return base.ToString() + "<" + state + ">";
        }
    }
}
