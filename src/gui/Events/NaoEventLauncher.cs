using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.GUI.Events.Parameters;
using Naovigate.Util;
using System.Drawing;
using System.Windows.Forms;

namespace Naovigate.GUI.Events
{
    class NaoEventLauncher : EventLauncher
    {
        public NaoEventLauncher() : base()
        {
            string Locations = "Locations";
            string ObjectID = "ObjectID";
            string Speech = "Speech";

            Customize("Post to Nao",
                new Dictionary<String,Constructor>() 
                {
                    { "ExitEvent", 
                        new Constructor(
                            () => new ExitEvent()) },
                    { "GoToEvent", 
                        new Constructor(
                            () => new GoToEvent(GetParameter<List<Point>>(Locations)),
                            new UserParameter<List<Point>>(Locations)) },
                    { "HaltEvent", 
                        new Constructor(
                            () => new HaltEvent()) },
                    { "PickupEvent", 
                        new Constructor(
                            () => new PickupEvent(GetParameter<int>(ObjectID)),
                            new UserParameter<int>(ObjectID)) },
                    { "PutDownEvent", 
                        new Constructor(
                            () => new PutDownEvent()) },
                    { "SayEvent", 
                        new Constructor(
                            () => new SayEvent(GetParameter<string>(Speech)),
                            new UserParameter<string>(Speech)) },
                });
        }

        protected override void PopulateParameterMap()
        {
            base.PopulateParameterMap();
            AddParameterMapping(typeof(List<Point>), 
                () => new LocationsChooser() as IParamChooser);
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
