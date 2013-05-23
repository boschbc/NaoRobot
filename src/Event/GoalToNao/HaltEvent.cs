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
        public new static readonly EventCode code = EventCode.Halt;
        public HaltEvent() : base(Priority.Medium) { }
        
        
        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code); ;
            try
            {
                Walk.Instance.Abort();
                Grabber.Abort();
            }
            catch
            {
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         * The Halt Event cannot be aborted (has no effect).
         */
        public override void Abort() { }
    }
}
