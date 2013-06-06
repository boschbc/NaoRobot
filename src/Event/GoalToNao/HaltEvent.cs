using System;
using System.Collections.Generic;
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
    /// - Remove all pending event from the EventQueue, and fires FailureEvents for them.
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
                EventQueue.Nao.Suspend();
                // stop these in all cases
                Walk.Instance.StopMoving();
                Grabber.Instance.Abort();

                // stop all event in the queue, including the currently fired one.
                INaoEvent cur = EventQueue.Nao.Current;
                if (cur != null) cur.Abort();
                List<INaoEvent> events = EventQueue.Nao.ClearAndGet();
                foreach(INaoEvent e in events){
                    if (e != null)
                    {
                        e.Abort();
                        EventQueue.Goal.Post(new FailureEvent(e.EventCode));
                    }
                }

                // and continue as normal
                EventQueue.Nao.Resume();
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

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Halt; }
        }
    }
}
