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
        private bool aborted;

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
            stream.WriteByte(eventCode);
        }

        /// <summary>
        /// Transmits this event's data as integers.
        /// </summary>
        public void SendAsInt()
        {
            if (!aborted)
            {
                SendCode();
                for (int i = 0; i < data.Length; i++)
                    stream.WriteInt(data[i]);
            }
        }

        /// <summary>
        /// Transmits this event's data as bytes.
        /// </summary>
        public void SendAsByte()
        {
            if (!aborted)
            {
                SendCode();
                for (int i = 0; i < data.Length; i++)
                    stream.WriteByte((byte)(0xFF & data[i]));
            }
        }

        /// <summary>
        /// Sends information across the stream.
        /// </summary>
        protected virtual void Send() 
        {
            SendAsInt();
            if (stream.GetType() == typeof(BitStringCommunicationStream))
                stream.WriteNewline();
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
            catch (Exception e)
            {
                Logger.Log(this, "Failed to fire: " + e);
            }
        }

        /// <summary>
        /// Aborts this event's operation.
        /// </summary>
        public override void Abort()
        {
            aborted = true;
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
