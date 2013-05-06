using System;
using Naovigate.Communication;
namespace Naovigate.Event.GoalToNao
{
    /**
     * @param MarkerID the id of a marker.
     * @param Distance the Nao will stop Distance rooms away from the marker.
     * Move to the marker MarkerID, if it is in range of the Nao.
     * The Nao will stop Distance rooms away from the marker.
     */
    public class GoToNaoEvent : GoalToNaoEvent
    {
        public GoToNaoEvent(CommunicationStream stream)
            : base(stream)
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
        public override void Abort()
        {

        }

        /**
        * Takes a communication stream and extracts different parameters as required.
        **/
        protected override void Unpack(CommunicationStream stream)
        {

        }
    }
}
