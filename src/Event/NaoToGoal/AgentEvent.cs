using System;
using System.Collections.Generic;

using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send the Nao id. The id consists of the last number of the ip address.
     */
    public abstract class AgentEvent : NaoEvent
    {
        
        private int id;

        /*
         * Explicit constructor.
         */
        public AgentEvent(int id)
        {
            this.id = id;
        }

        /*
         * Implicit constructor.
         */
        public AgentEvent()
        {
            byte[] bytes = NaoState.IP.GetAddressBytes();
            id = bytes[bytes.Length - 1];
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            //Send over the network to goal
        }
    }
}
