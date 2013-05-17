﻿using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Haptics;
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
         */
        public override void Abort()
        {
            // The halt event cannot be aborted and therefore this method remains empty.
        }
    }
}
