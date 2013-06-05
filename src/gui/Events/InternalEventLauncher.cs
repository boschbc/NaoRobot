using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Event;
using Naovigate.Event.Internal;
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
            string Rotation = "Rotation";
            string Accuracy = "Accuracy";
            string VelocityX = "VelocityX";
            string VelocityY = "VelocityY";
            
            Customize("Post to Internal",
                new Dictionary<String, Constructor>() 
                {
                    { "CrouchEvent", 
                        new Constructor(
                            () => new CrouchEvent()) },
                    { "GrabEvent", 
                        new Constructor(
                            () => new GrabEvent()) },
                    { "MoveEvent", 
                        new Constructor(
                            () => new MoveEvent(GetParameter<float>(VelocityX), 
                                                GetParameter<float>(VelocityY),
                                                GetParameter<float>(Rotation)),
                            new UserParameter<float>(VelocityX),
                            new UserParameter<float>(VelocityY),
                            new UserParameter<float>(Rotation)) },
                    { "TurnEvent", 
                        new Constructor(
                            () => new TurnEvent(GetParameter<float>(Rotation),
                                                GetParameter<float>(Accuracy)),
                            new UserParameter<float>(Rotation),
                            new UserParameter<float>(Accuracy)) },
                    { "ShutdownEvent", 
                        new Constructor(
                            () => new ShutdownEvent()) },
                    { "SitDownEvent", 
                        new Constructor(
                            () => new SitDownEvent()) },
                    { "StandUpEvent", 
                        new Constructor(
                            () => new StandUpEvent()) },
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
