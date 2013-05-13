using System;
using Naovigate.Communication;
namespace Naovigate.Event.GoalToNao
{
    /*
     * @param ID
     * Pick up the object with id ID
     */
    public class PickupEvent : NaoEvent
    {
        private int id;

        /*
         * Default constructor.
         */
        public PickupEvent()
        {
            Unpack();
        }

        /*
         * Explicit constructor.
         */
        public PickupEvent(int id)
        {
            this.id = id;
        }

        /*
         * Takes a communication stream and extracts different parameters as required.
         */
        private void Unpack()
        {
            id = stream.ReadInt();
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
        public override  void Abort()
        {

        }

       

    }
}
