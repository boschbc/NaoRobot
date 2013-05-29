using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Movement;
using Naovigate.Vision;

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
        public HaltEvent() : base(Priority.Medium) { }
        
        
        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code); ;
            try
            {
                Walk.Instance.StopMove();
                Grabber.Instance.Abort();
            }
            catch
            {
                statusEvent = new FailureEvent(code);
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
