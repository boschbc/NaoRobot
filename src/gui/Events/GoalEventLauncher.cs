using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    class GoalEventLauncher : EventLauncher
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
                            () => new SeeEvent(GetParameter<int>(ObjectID),
                                               GetParameter<int>(Location)),
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
            
        
        protected override void PostEvent(INaoEvent goalEvent)
        {
            if (GoalCommunicator.Instance.Running)
                EventQueue.Goal.Post(goalEvent);
            else
                Logger.Log(this, 
                    "Cannot post " + goalEvent + 
                    ", goal communicator is not connected to any server.");
        }
    }
}
