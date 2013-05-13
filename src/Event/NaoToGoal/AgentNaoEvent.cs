using System;
using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Send the Nao id. The id consists of the last number of the ip address.
     */
    public class AgentNaoEvent : DataSendingNaoEvent
    {
        private static readonly byte eventID = 0x8e;

        /**
        * create a new LocationNaoEvent, with the specified locationID to be sent
        */
        public AgentNaoEvent() : base(eventID, GetID()) { }

        private static int GetID()
        {
            String ip = NaoState.IP.ToString();
            int dot = ip.LastIndexOf('.');
            int res = 0;
            Int32.TryParse(ip.Substring(dot, ip.Length), out res);
            return res;
        }

        /**
         * Fires the event.
         **/
        public override void Fire()
        {
            SendAsInt();
        }
    }
}
