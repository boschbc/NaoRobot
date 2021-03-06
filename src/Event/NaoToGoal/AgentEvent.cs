﻿
using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Send the Nao ID.
    /// </summary>
    public sealed class AgentEvent : DataSendingNaoEvent
    {
        /// <summary>
        /// Implicit constructor.
        /// </summary>
        public AgentEvent() : base((byte) EventCode.Agent, AgentEvent.ID) { }

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="id"></param>
        public AgentEvent(int id)
            : base((byte)EventCode.Agent, id) { }

        /// <summary>
        /// Get the ID of the currently connected to Nao agent.
        /// </summary>
        private static int ID
        {
            get { return NaoState.Instance.IP.GetHashCode(); }
        }
    }
}
