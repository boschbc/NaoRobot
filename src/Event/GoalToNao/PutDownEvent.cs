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

        private ActionExecutor executor;
        private bool started;
        private bool done;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PutDownEvent() 
        {
            done = false;
        }

        /// <summary>
        /// True if the event's execution has been started.
        /// </summary>
        public bool Started
        {
            get { return started; }
            private set { started = value; }
        }

        /// <summary>
        /// True if the event's execution has finished.
        /// </summary>
        public bool Finished
        {
            get { return done; }
            private set { done = value; }
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            try
            {
                Logger.Log(this, "PutDownWorker");
                executor = Grabber.Instance.PutDown();
                executor.Start();
                Logger.Log(this, "Wait");
                executor.WaitFor();
                Logger.Log(this, "Done");
            }
            catch(Exception e)
            {
                Logger.Log(this, e.Message);
                StatusCheck();
            }
        }

        /// <summary>
        /// Checks whether execution was succesful, and posts a response event accordingly.
        /// </summary>
        private void StatusCheck()
        {
            INaoEvent statusEvent = new SuccessEvent(code);
            
            if (executor != null && executor.Error is InvalidOperationException)
            {
                statusEvent = new FailureEvent(code);
            }
            else
            {
                try
                {
                    if (Grabber.Instance.HoldingObject())
                        statusEvent = new FailureEvent(code);
                }
                catch
                {
                    statusEvent = new ErrorEvent();
                }
            }
            EventQueue.Goal.Post(statusEvent);
            Finished = true;
        }

        public override void WaitFor()
        {
            if(executor != null) executor.WaitFor();  
        }

        /// <summary>
        /// Aborts this event's execution.
        /// </summary>
        public override void Abort()
        {
            base.Abort();
            if (executor != null)
            {
                executor.Abort();
            }

        }
    }
}
