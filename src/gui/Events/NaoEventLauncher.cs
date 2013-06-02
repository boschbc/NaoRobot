using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    class NaoEventLauncher : EventLauncher
    {
        public NaoEventLauncher() : base()
        {
            Customize("Post to Nao",
                new List<Func<INaoEvent>>() 
            {
                () => new ExitEvent(),
                () => new HaltEvent(),
                () => new PickupEvent(1),
                () => new PutDownEvent(),
                () => new SayEvent("I am a toy")
            });
        }

        protected override void PostEvent(INaoEvent naoEvent)
        {
            if (NaoState.Instance.Connected)
                EventQueue.Nao.Post(naoEvent);
        }
    }
}
