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
                new List<Func<INaoEvent>>()
                {
                    () => new AgentEvent(0),
                    () => new DistanceToEvent(143),
                    () => new ErrorEvent(),
                    () => new FailureEvent(EventCode.GoTo),
                    () => new HoldingEvent(0),
                    () => new LocationEvent(0),
                    () => new SeeEvent(0, 0),
                    () => new StateEvent(0),
                    () => new SuccessEvent(EventCode.GoTo)
                });
        }
            
        
        protected override void PostEvent(INaoEvent goalEvent)
        {
            EventQueue.Goal.Post(goalEvent);
            //else
            //    Logger.Log(this, "Cannot post event: " + goalEvent + 
            //                     ". Not connected to Goal Server.");

        }
    }
}
