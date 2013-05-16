using System;
using Naovigate.Communication;

namespace Naovigate.Event.NaoToGoal
{
    /**
     * 
     */
    public abstract class DataSendingNaoEvent : NaoEvent
    {
        private byte eventID = 0x00;
        private int[] data;
        private bool aborted;

        /**
        * create a new IdSendingNaoEvent, which sends some given ids to Goal.
        * used to reduce code duplication
        */
        public DataSendingNaoEvent(byte eventID, params int[] data)
        {
            this.eventID = eventID;
            this.data = data;
        }

        private void SendCode()
        {
            stream.Stream.WriteByte(eventID);
        }

        public void SendAsInt()
        {
            if (!aborted)
            {
                SendCode();
                for (int i = 0; i < data.Length; i++)
                    stream.WriteInt(data[i]);
            }
        }

        public void SendAsByte()
        {
            if (!aborted)
            {
                SendCode();
                for (int i = 0; i < data.Length; i++)
                    stream.Stream.WriteByte((byte)(0xFF & data[i]));
            }
        }

        /*
         * Fire this event.
         */
        public override void Fire()
        {
            SendAsInt();
        }

        /*
         * Aborts this event's operation.
         */
        public override void Abort()
        {
            aborted = true;
        }
    }
}
