using System;
using System.Collections.Generic;
using System.Drawing;

using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.GUI.Events.Parameters;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A control that allows the user to launch nao-related events.
    /// </summary>
    public sealed class NaoEventLauncher : EventLauncher
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

        /// <summary>
        /// Adds custom mappings to the parameter-chooser map.
        /// </summary>
        protected override void PopulateWithCustoms()
        {
            base.PopulateWithCustoms();
            AddParameterMapping(typeof(List<Point>), 
                () => new LocationsChooser() as IParamChooser);
        }

        /// <summary>
        /// Posts the selected event to the Nao event-queue.
        /// </summary>
        /// <param name="naoEvent">The selected event.</param>
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
