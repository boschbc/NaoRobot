using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    public abstract class ReportBackEvent : NaoEvent
    {
        /// <summary>
        /// Create a no-arguments-required event.
        /// The event's priority is medium.
        /// </summary>
        public ReportBackEvent() : base() { }

        /// <summary>
        /// Creates an empty NaoEvent instance with the specified priority.
        /// </summary>
        /// <param name="p">A priority. Either Low, Medium or High.</param>
        public ReportBackEvent(Priority p) : base(p) { }

        /// <summary>
        /// Creates an empty NaoEvent instance with the specified execution behavior.
        /// </summary>
        /// <param name="e">A behavior. Either Durative or Instantaneous.</param>
        public ReportBackEvent(ExecutionBehavior e) : base(e) { }

        /// <summary>
        /// Creates an empty NaoEvent instance with the specified priority and execution behavior.
        /// </summary>
        /// <param name="p">A priority. Either Low, Medium or High.</param>
        /// <param name="e">A behavior. Either Durative or Instantaneous.</param>
        public ReportBackEvent(Priority p, ExecutionBehavior e) : base(p, e) { }

        /// <summary>
        /// Posts a failure event to the goal event-queue.
        /// </summary>
        protected virtual void ReportFailure()
        {
            EventCode.Log(this);
            EventQueue.Goal.Post(new FailureEvent(EventCode));
        }

        /// <summary>
        /// Posts a success event to the goal event-queue.
        /// </summary>
        protected virtual void ReportSuccess()
        {
            Logger.Log(this, "@@@@@@@@@@@@@ = "+(int)EventCode);
            EventQueue.Goal.Post(new SuccessEvent(EventCode));
        }

        /// <summary>
        /// Posts an error event to the goal event-queue.
        /// </summary>
        protected virtual void ReportError()
        {
            EventQueue.Goal.Post(new ErrorEvent());
        }
    }
}
