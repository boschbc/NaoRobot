﻿using System;
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
        /// Extract the MarkerID and Distance parameters from the communication stream.
        /// </summary>
        private void Unpack()
        {
            text = stream.ReadString();
        }
        
        /// <summary>
        /// Say the received text.
        /// </summary>
        public override void Fire()
        {
            Proxies.GetProxy<Aldebaran.Proxies.TextToSpeechProxy>().say(text);
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Say; }
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

