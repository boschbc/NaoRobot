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
        private readonly string VelocityX = "VelocityX";
        private readonly string VelocityY = "VelocityY";
        private readonly string Rotation = "Rotation";
        private readonly string Accuracy = "Accuracy";
        private readonly string ID = "ID";
        private readonly string Distance = "Distance";

        private Dictionary<String, Constructor> constructorByName;

        public InternalEventLauncher()
            : base()
        {
            constructorByName = new Dictionary<string, Constructor>();
            AddConstructors();
            Customize("Post to Internal", constructorByName);
        }

        private void AddConstructors()
        {
            AddUtilConstructors();
            AddPoseConstructors();
            AddGrabbingConstructors();
            AddMovementConstructors();
        }

        private void AddPoseConstructors()
        {
            constructorByName.Add("SitDownEvent",
                new Constructor(() => new SitDownEvent()));
            constructorByName.Add("StandUpEvent",
                new Constructor(() => new StandUpEvent()));
            constructorByName.Add("CrouchEvent",
                new Constructor(() => new CrouchEvent()));
        }

        private void AddMovementConstructors()
        {
            constructorByName.Add("MoveEvent",
                new Constructor(
                    () => new MoveEvent(GetParameter<float>(VelocityX),
                                        GetParameter<float>(VelocityY),
                                        GetParameter<float>(Rotation)),
                    new UserParameter<float>(VelocityX),
                    new UserParameter<float>(VelocityY),
                    new UserParameter<float>(Rotation)));
            constructorByName.Add("GoToMarkerEvent",
                 new Constructor(
                    () => new GoToMarkerEvent(GetParameter<int>(ID),
                                              GetParameter<int>(Distance)),
                    new UserParameter<int>(ID),
                    new UserParameter<int>(Distance)));
            constructorByName.Add("TurnEvent",
                new Constructor(
                    () => new TurnEvent(GetParameter<float>(Rotation),
                                        GetParameter<float>(Accuracy)),
                    new UserParameter<float>(Rotation),
                    new UserParameter<float>(Accuracy)));
        }

        private void AddGrabbingConstructors()
        {
            constructorByName.Add("GrabEvent",
                new Constructor(() => new GrabEvent()));
        }

        private void AddUtilConstructors()
        {
            constructorByName.Add("Test",
                new Constructor(() => new TestEvent()));
            constructorByName.Add("ShutdownEvent",
                new Constructor(() => new ShutdownEvent()));
        }

        protected override void PostEvent(INaoEvent naoEvent)
        {
            base.PostEvent(naoEvent);
            if (NaoState.Instance.Connected)
                EventQueue.Nao.Post(naoEvent);
            else
                Logger.Log(this,
                    "Cannot post " + naoEvent + ", NaoState is not connected to any Nao.");
        }
    }
}
