using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;

namespace Naovigate.Event.GoalToNao
{
    /*
     * Drop the object the Nao is holding. If the Nao is not holding an object, nothing happens.
     */
    public class PutDownEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.PutDown;
        /*
         * Default constructor.
         */
        public PutDownEvent()
        {

        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
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

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
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
