using System;
using System.Collections.Generic;
using Naovigate.Event;
using Naovigate.Event.Internal;
using Naovigate.Navigation;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A control that allows the user to choose and launch internal events.
    /// </summary>
    public sealed class InternalEventLauncher : EventLauncher
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

        /// <summary>
        /// Adds the different constructor categories to the internal list.
        /// </summary>
        private void AddConstructors()
        {
            AddUtilConstructors();
            AddPoseConstructors();
            AddGrabbingConstructors();
            AddMovementConstructors();
        }

        /// <summary>
        /// Adds pose-related event-constructors to the internal list.
        /// </summary>
        private void AddPoseConstructors()
        {
            constructorByName.Add("SitDownEvent",
                new Constructor(() => new SitDownEvent()));
            constructorByName.Add("StandUpEvent",
                new Constructor(() => new StandUpEvent()));
            constructorByName.Add("CrouchEvent",
                new Constructor(() => new CrouchEvent()));
        }

        /// <summary>
        /// Adds movement-related event-constructors to te internal list.
        /// </summary>
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
            constructorByName.Add("TurnRelativeEvent",
                new Constructor(
                    () => new TurnRelativeEvent(GetParameter<float>(Rotation),
                                        GetParameter<float>(Accuracy)),
                    new UserParameter<float>(Rotation),
                    new UserParameter<float>(Accuracy)));
            constructorByName.Add("TurnAbsoluteEvent",
                new Constructor(
                    () => new TurnAbsoluteEvent(GetParameter<Direction>("Direction")),
                    new UserParameter<Direction>("Direction")));
        }

        /// <summary>
        /// Adds grabbing-related constructor's to the internal event list.
        /// </summary>
        private void AddGrabbingConstructors()
        {
            constructorByName.Add("GrabEvent",
                new Constructor(() => new GrabEvent()));
        }

        /// <summary>
        /// Adds utility-related constructor's to the internal event list.
        /// </summary>
        private void AddUtilConstructors()
        {
            constructorByName.Add("Test",
                new Constructor(() => new TestEvent()));
            constructorByName.Add("Report",
                new Constructor(() => new ReportEvent()));
            constructorByName.Add("ShutdownEvent",
                new Constructor(() => new ShutdownEvent()));
            constructorByName.Add("Performance",
                            new Constructor(() => new PerformanceEvent()));
        }

        /// <summary>
        /// Posts an event to the Nao event-queue.
        /// </summary>
        /// <param name="naoEvent">A Nao event.</param>
        protected override void PostEvent(INaoEvent naoEvent)
        {
            base.PostEvent(naoEvent);
            Logger.Log(this, naoEvent);
            if (NaoState.Instance.Connected)
                EventQueue.Nao.Post(naoEvent);
            else
                Logger.Log(this,
                    "Cannot post " + naoEvent + ", NaoState is not connected to any Nao.");
        }
    }
}
