using System;
using Naovigate.Communication;
namespace Naovigate.Event.GoalToNao
{
    /*
     * @param MarkerID the id of a marker.
     * @param Distance the Nao will stop Distance rooms away from the marker.
     * Move to the marker MarkerID, if it is in range of the Nao.
     * The Nao will stop Distance rooms away from the marker.
     */
    public class GoToEvent : GoalToNaoEvent
    {
        private int markerID;
        private int distance;

        /*
         * Explicit constructor.
         */
        public GoToEvent(int markerID, int distance)
        {
            this.markerID = markerID;
            this.distance = distance;
        }

        /*
         * Inherited constructor.
         */
        public GoToEvent(CommunicationStream stream)
            : base(stream) { }

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

        /*
        * Extract the MarkerID and Distance parameters from a communication stream.
        */
        protected override void Read(CommunicationStream stream)
        {
            markerID = stream.ReadInt();
            distance = stream.ReadInt();
        }
    }
}
