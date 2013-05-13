using System;
using Naovigate.Communication;
namespace Naovigate.Event.GoalToNao
{
    /**
     * @param ID
     * Pick up the object with id ID
     */
    public class PickupNaoEvent : GoalToNaoEvent
    {
        public PickupNaoEvent()
        {

        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public override void Fire()
        {

        }

        /**
         * See the INaoEvent class docs for documentation of this method.
         **/
        public override  void Abort()
        {

        }

        /**
        * Takes a communication stream and extracts different parameters as required.
        **/
        protected override void Unpack()
        {

        }

    }
}
