using System;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    public class SayEvent : NaoEvent
    {
        private string text;
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
            NaoState.Instance.SpeechProxy.say(text);
        }
    }
}

