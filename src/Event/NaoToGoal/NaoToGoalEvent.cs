using System;
using System.Collections.Generic;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Describes a NaoEvent from the Nao client to the Goal client.
     */
    public abstract class NaoToGoalEvent : NaoEvent
    {
        
        /*
         * Default constructor.
         */
        public NaoToGoalEvent() { }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override abstract void Fire();

        /*
         * See the INaoEvent class docs for documentation of this method.
         * Nao to Goal events are instantanious, once fired. Therefore the Abort() method remains unimplemented.
         */
        public override void Abort() { }
    }
}
