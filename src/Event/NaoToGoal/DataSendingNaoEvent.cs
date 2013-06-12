using System;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// A class representing an event destined to be sent to the GOAL-server.
    /// </summary>
    public class DataSendingNaoEvent : NaoEvent
    {
        private byte eventCode = 0x00;
        private int[] data;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="eventCode">This event's code. Passed on by any sub-class.</param>
        /// <param name="data">One or more integers.</param>
        public DataSendingNaoEvent(byte eventCode, params int[] data)
        {
            this.eventCode = eventCode;
            this.data = data;
        }

        /// <summary>
        /// Overload.
        /// </summary>
        public DataSendingNaoEvent(EventCode eventCode, params int[] data) : this((byte)eventCode, data) { }

        /// <summary>
        /// Transmits this event's code.
        /// </summary>
        private void SendCode()
        {
            Stream.WriteByte(eventCode);
        }

        /// <summary>
        /// Transmits this event's data as integers.
        /// </summary>
        public void SendAsInt()
        {
            if (!Aborted)
            {
                SendCode();
                for (int i = 0; i < data.Length; i++)
                    Stream.WriteInt(data[i]);
            }
        }

        /// <summary>
        /// Transmits this event's data as bytes.
        /// </summary>
        public void SendAsByte()
        {
            if (!Aborted)
            {
                SendCode();
                for (int i = 0; i < data.Length; i++)
                    Stream.WriteByte((byte)(0xFF & data[i]));
            }
        }

        /// <summary>
        /// Sends information across the stream.
        /// </summary>
        protected virtual void Send() 
        {

            SendAsInt();
            if (Stream.GetType() == typeof(BitStringCommunicationStream))
            {
                Stream.WriteNewline();
            }
            
        }

        /// <summary>
        /// Fire this event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                Send();
            }
            catch
            {
                Logger.Log(this, "Failed to fire.");
            }
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return (EventCode)eventCode; }
        }
    }
}
