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
    public class GoToEvent : NaoEvent
    {
        private int markerID;
        private int distance;

        /*
         * Default constructor.
         */
        public GoToEvent() { }

        /*
         * Explicit constructor.
         */
        public GoToEvent(int markerID, int distance)
        {
            this.markerID = markerID;
            this.distance = distance;
        }

        /*
         * Extract the MarkerID and Distance parameters from a communication stream.
         */
        private void Unpack()
        {
            markerID = stream.ReadInt();
            distance = stream.ReadInt();
        }

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
