using System;
using Naovigate.Communication;

namespace Naovigate.Event.GoalToNao
{
    /*
     * Drop the object the Nao is holding. If the Nao is not holding an object, nothing happens.
     */
    public class PutDownEvent : GoalToNaoEvent
    {
        
        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {

        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Abort()
        {

        }
    }
}
