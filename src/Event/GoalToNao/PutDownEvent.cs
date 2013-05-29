using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// Drop the object the Nao is holding. If the Nao is not holding an object, nothing happens.
    /// </summary>
    public class PutDownEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.PutDown;

        private ActionExecutor worker;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PutDownEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code);
            try
            {
                
                worker = Grabber.Instance.PutDown();
                worker.WaitFor();  //Check if any exceptions are thrown
                if (worker.Aborted)
                    if (Grabber.Instance.HoldingObject())
                        statusEvent = new FailureEvent(code);
            }
            catch
            {
                Logger.Log(this, "at first catch");
                try
                {
                    /// If we are not holding the object, than the exception 
                    /// wasn't so bad, but if we still do:
                    if (Grabber.Instance.HoldingObject())
                        statusEvent = new FailureEvent(code);
                }
                catch
                {
                    //Something is seriously wrong with Grabber.HoldingObject
                    statusEvent = new ErrorEvent();
                }
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /// <summary>
        /// Aborts this event's execution.
        /// </summary>
        public override void Abort()
        {
            worker.Abort();
        }
    }
}
