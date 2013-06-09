using System;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    public class SayEvent : NaoEvent
    {
        private string text;

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="text">The text to say.</param>
        public SayEvent(string text) : base(ExecutionBehavior.Instantaneous)
        {
            this.text = text;
        }

        /// <summary>
        /// This event's byte-code as defined by the GOAL-Nao API.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Say; }
        }

        /// <summary>
        /// Extract the MarkerID and Distance parameters from the communication stream.
        /// </summary>
        private void Unpack()
        {
            text = Stream.ReadString();
        }
        
        /// <summary>
        /// Say the received text.
        /// </summary>
        public override void Fire()
        {
            NaoState.Instance.SpeechProxy.say(text);
        }

        /// <summary>
        /// Returns a string representation of this event.
        /// </summary>
        /// <returns>A human-readable string.</returns>
        public override string ToString()
        {
            return base.ToString() + "<" + text + ">";
        }
    }
}

