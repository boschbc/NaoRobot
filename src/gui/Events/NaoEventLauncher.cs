using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.GUI.Popups.ParamChooser;
using Naovigate.Util;
using System.Drawing;
using System.Windows.Forms;

namespace Naovigate.GUI.Events
{
    class NaoEventLauncher : EventLauncher
    {
        public NaoEventLauncher() : base()
        {

            Customize("Post to Nao",
                new Dictionary<String,Func<INaoEvent>>() 
            {
                { "ExitEvent", () => new ExitEvent() },
                { "GoToEvent", () => new GoToEvent(UserParameter<List<Point>>()) },
                { "HaltEvent", () => new HaltEvent() },
                { "PickupEvent", () => new PickupEvent(UserParameter<int>()) },
                { "PutDownEvent", () => new PutDownEvent() },
                { "SayEvent", () => new SayEvent(UserParameter<string>()) }
            });
        }

        protected override void InitializeParameterMap()
        {
            Logger.Log();
            base.InitializeParameterMap();
            AddParameterMapping(typeof(List<Point>), AskUserForLocationList);
        }

        private Object AskUserForLocationList()
        {
            return DisplayPopup(new LocationsChooser());
        }

        protected override void PostEvent(INaoEvent naoEvent)
        {
            if (NaoState.Instance.Connected)
                EventQueue.Nao.Post(naoEvent);
            else
                Logger.Log(this, 
                    "Cannot post " + naoEvent + ", NaoState is not connected to any Nao.");
        }
    }
}
