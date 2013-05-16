using System;
using System.Collections.Generic;

using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send the Nao id. The id consists of the last number of the ip address.
     */
    public class AgentEvent : DataSendingNaoEvent
    {
        /*
         * Explicit constructor.
         */
        public AgentEvent(int id) : base((byte) EventCode.Agent, AgentEvent.ID) { }

        /*
         * Get the ID of the currently connected to Nao agent.
         */
        private static int ID
        {
            get { return NaoState.IP.GetHashCode(); }
        }
    }
}
