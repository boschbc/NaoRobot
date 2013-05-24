using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// Drop the object the Nao is holding. If the Nao is not holding an object, nothing happens.
    /// </summary>
    public class PutDownEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.PutDown;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PutDownEvent()
        {

        }

        /// <summary>
        /// See the INaoEvent class docs for documentation of this method.
        /// </summary>
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code);
            try
            {
                Grabber.Instance.PutDown();
            }
            catch
            {
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /// <summary>
        /// See the INaoEvent class docs for documentation of this method.
        /// </summary>
        public override void Abort()
        {
            try
            {
                Grabber.Abort();
            }
            catch
            {
                EventQueue.Nao.Enqueue(new ErrorEvent());
            }
        }
    }
}
