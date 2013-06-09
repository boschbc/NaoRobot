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
    /// - Remove all pending events from the EventQueue, and fires FailureEvents for them.
    /// </summary>
    public class HaltEvent : ReportBackEvent
    {
        public HaltEvent() : base(Priority.Medium, ExecutionBehavior.Instantaneous) { }
        
        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                EventQueue.Nao.Suspended = true;
                HaltNao();
                AbortCurrentEvent();
                ClearEventQueue();
                EventQueue.Nao.Suspended = false;
                ReportSuccess();
            }
            catch
            {
                //If the Nao can't halt then there is something serious going on:
                ReportError();
            }
        }

        /// <summary>
        /// Halts any moving/grabbing operation the Nao is currently doing.
        /// </summary>
        private void HaltNao()
        {
            Walk.Instance.StopMoving();
            Grabber.Instance.Abort();
        }

        /// <summary>
        /// Aborts the event that is currently being fired (if any).
        /// </summary>
        private void AbortCurrentEvent()
        {
            if (EventQueue.Nao.Current != null)
                EventQueue.Nao.Current.Abort();
        }

        /// <summary>
        /// Clears the Nao event-queue and emits a failure event for each event that was cleared.
        /// </summary>
        private void ClearEventQueue()
        {
            List<INaoEvent> events = EventQueue.Nao.ClearAndGet();
            foreach (INaoEvent e in events)
            {
                if (e != null)
                {
                    e.Abort();
                    EventQueue.Goal.Post(new FailureEvent(e.EventCode));
                }
            }
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
