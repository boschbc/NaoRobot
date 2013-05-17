using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Haptics;

namespace Naovigate.Event.GoalToNao
{
    /*
     * Drop the object the Nao is holding. If the Nao is not holding an object, nothing happens.
     */
    public class PutDownEvent : NaoEvent
    {
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
            NaoEvent statusEvent = new SuccessEvent(EventQueue.Instance.GetID(this)); ;
            try
            {
                Grabber.Instance.PutDown();
            }
            catch
            {
                statusEvent = new FailureEvent(EventQueue.Instance.GetID(this));
            }
            EventQueue.Instance.Enqueue(statusEvent);
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
                EventQueue.Instance.Enqueue(new ErrorEvent());
            }
        }
    }
}
