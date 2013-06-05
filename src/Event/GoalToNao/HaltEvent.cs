using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Movement;
using Naovigate.Vision;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// Stop all actions the Nao is doing:
    /// - Aborts any grabbing operations.
    /// - Aborts any movement operations.
    /// </summary>
    public class HaltEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Halt;
        public HaltEvent() : base(Priority.Medium, ExecutionBehavior.Instantaneous) { }
        
        
        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code); ;
            try
            {
                EventQueue.Nao.Clear();
                if (EventQueue.Nao.CurrentlyFiring != null)
                    EventQueue.Nao.CurrentlyFiring.Abort();
            }
            catch
            {
                //If the Nao can't halt then there is something serious going on:
                statusEvent = new ErrorEvent();
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /// <summary>
        /// Aborts this event's execution.
        /// The Halt Event cannot be aborted (has no effect).
        /// </summary>
        public override void Abort() { }
    }
}
