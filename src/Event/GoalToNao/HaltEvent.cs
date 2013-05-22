using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Movement;
using Naovigate.Vision;

namespace Naovigate.Event.GoalToNao
{
    /*
     * Stop all actions the Nao is doing:
     *  - Aborts any grabbing operations.
     *  - Aborts any movement operations.
     *  - Deactivates the sonar.
     */
    public class HaltEvent : NaoEvent
    {
        public HaltEvent() : base(Priority.Medium) { }
        
        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(EventQueue.Instance.GetID(this)); ;
            try
            {
                Grabber.Abort();
                Walk.Instance.Abort();
                Sonar.Instance.Deactivate();
            }
            catch
            {
                statusEvent = new FailureEvent(EventQueue.Instance.GetID(this));
            }
            EventQueue.Instance.Enqueue(statusEvent);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         * The Halt Event cannot be aborted (has no effect).
         */
        public override void Abort() { }
    }
}
