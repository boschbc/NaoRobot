using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Event;
using Naovigate.Event.Internal;
using Naovigate.GUI.Popups.ParamChooser;
using Naovigate.Util;
using System.Drawing;
using System.Windows.Forms;

namespace Naovigate.GUI.Events
{
    public class InternalEventLauncher : EventLauncher
    {
        public InternalEventLauncher()
            : base()
        {

            Customize("Post to Internal",
                new Dictionary<String, Func<INaoEvent>>() 
                {
                    { "CrouchEvent", () => new CrouchEvent() },
                    { "GrabEvent", () => new GrabEvent() },
                    { "MoveEvent", () => new MoveEvent(UserParameter<float>(), 
                                                       UserParameter<float>(),
                                                       UserParameter<float>()) },
                    { "ShutdownEvent", () => new ShutdownEvent() },
                    { "SitDownEvent", () => new SitDownEvent() },
                    { "StandUpEvent", () => new StandUpEvent() },
                    { "GoToMarkerEvent", () => new GoToMarkerEvent(UserParameter<int>(), UserParameter<int>()) }
                });
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
