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
            Customize("Post to Goal",
                new Dictionary<String, Func<INaoEvent>>()
                {
                    { "AgentEvent", () => new AgentEvent(UserParameter<int>()) },
                    { "ErrorEvent", () => new ErrorEvent() },
                    { "FailureEvent", () => new FailureEvent(EventCode.GoTo) },
                    { "HoldingEvent", () => new HoldingEvent(UserParameter<int>()) },
                    { "LocationEvent", () => new LocationEvent(UserParameter<int>()) },
                    { "SeeEvent", () => new SeeEvent(UserParameter<int>(), UserParameter<int>()) },
                    { "StateEvent", () => new StateEvent(UserParameter<int>()) },
                    { "SuccessEvent", () => new SuccessEvent(EventCode.GoTo) }
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
