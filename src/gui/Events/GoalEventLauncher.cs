using System;
using System.Collections.Generic;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    /// <summary>0
    /// A control that allows the user to select and launch Goal related events.
    /// </summary>
    sealed class GoalEventLauncher : EventLauncher
    {
        public GoalEventLauncher() : base() 
        {
            string AgentID = "AgentID";
            string ObjectID = "ObjectID";
            string RoomID = "RoomID";
            string Code = "Code";
            string State = "State";
            string Location = "Location";

            Customize("Post to Goal",
                new Dictionary<String, Constructor>()
                {
                    { "AgentEvent", 
                        new Constructor(
                            () => new AgentEvent(GetParameter<int>(AgentID)),
                            new UserParameter<int>(AgentID)) },
                    { "ErrorEvent", 
                        new Constructor(
                            () => new ErrorEvent()) },
                    { "FailureEvent", 
                        new Constructor(
                            () => new FailureEvent(GetParameter<byte>(Code)),
                            new UserParameter<int>(Code)) },
                    { "HoldingEvent", 
                        new Constructor(
                            () => new HoldingEvent(GetParameter<int>(ObjectID)),
                            new UserParameter<int>(ObjectID)) },
                    { "LocationEvent", 
                        new Constructor(
                            () => new LocationEvent(GetParameter<int>(RoomID)),
                            new UserParameter<int>(RoomID)) },
                    { "SeeEvent", 
                        new Constructor(
                            () => new SeeEvent(GetParameter<int>(ObjectID)),
                            new UserParameter<int>(ObjectID),
                            new UserParameter<int>(Location)) },
                    { "StateEvent", 
                        new Constructor(
                            () => new StateEvent(GetParameter<int>(State)),
                            new UserParameter<int>(State)) },
                    { "SuccessEvent", 
                        new Constructor(
                            () => new SuccessEvent(GetParameter<byte>(Code)),
                            new UserParameter<int>(Code)) }
                });
        }
            
        /// <summary>
        /// Posts the selected event to the Goal event-queue.
        /// </summary>
        /// <param name="goalEvent">The event to post.</param>
        protected override void PostEvent(INaoEvent goalEvent)
        {
            base.PostEvent(goalEvent);
            if (GoalCommunicator.Instance.Running)
                EventQueue.Goal.Post(goalEvent);
            else
                Logger.Log(this, 
                    "Cannot post " + goalEvent + 
                    ", goal communicator is not connected to any server.");
        }
    }
}
