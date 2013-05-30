using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Util;
using System.Threading;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// Drop the object the Nao is holding. If the Nao is not holding an object, nothing happens.
    /// </summary>
    public class PutDownEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.PutDown;

        private ActionExecutor worker;
        private bool aborted = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PutDownEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            new Thread(new ThreadStart(WaitFor)).Start();
        }

        private void WaitFor()
        {
            NaoEvent statusEvent = new SuccessEvent(code);
            try
            {
                if (!aborted)
                {
                    worker = Grabber.Instance.PutDown();
                    worker.WaitFor();  //Check if any exceptions are thrown
                }
                else
                {
                    statusEvent = new FailureEvent(code);
                }
            }
            catch (ThreadInterruptedException)
            {
                if (Grabber.Instance.HoldingObject())
                {
                    statusEvent = new FailureEvent(code);
                }
            }
            catch (InvalidOperationException)
            {
                statusEvent = new FailureEvent(code);
            }
            catch
            {
                try
                {
                    /// If we are not holding the object, than the exception 
                    /// wasn't so bad, but if we still do:
                    if (Grabber.Instance.HoldingObject())
                    {
                        statusEvent = new FailureEvent(code);
                    }
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
            Logger.Log(this, "Aborting...");
            if (worker != null) worker.Abort();
            aborted = true;
        }
    }
}
