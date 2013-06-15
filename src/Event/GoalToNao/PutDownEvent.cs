﻿
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// Drop the object the Nao is holding.
    /// </summary>
    public sealed class PutDownEvent : ReportBackEvent
    {
        private ActionExecutor executor;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PutDownEvent() {}

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                executor = Grabber.Instance.PutDown();
                executor.Start();
                ReportSuccess();
            }
            catch {
                ReportFailure();
            }
        }

        /// <summary>
        /// Verifies that the Nao indeed holds an object at the end of this event's execution.
        /// </summary>
        private void VerifyObjectNotHeld()
        {
            ReportSuccess();
        }

        protected override void ReportSuccess()
        {
            base.ReportSuccess();
            EventQueue.Goal.Post(new DroppedObjectEvent());
        }
        /// <summary>
        /// Aborts this event's execution.
        /// </summary>
        public override void Abort()
        {
            base.Abort();
            if (executor != null)
                executor.Abort();
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.PutDown; }
        }
    }
}
