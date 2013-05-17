using System;
using Naovigate.Communication;
using Naovigate.Haptics;
using Naovigate.Movement;
using Naovigate.Util;
namespace Naovigate.Event.GoalToNao
{
    /*
     * @param ID
     * Pick up the object with id ID
     */
    public class PickupEvent : NaoEvent
    {
        private int id;
       // private ObjectSearchThread searchThread;

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
            // go to the object first
            searchThread = Walk.Instance.WalkTowardsObject(0,0,0);

            // grab the object
            Grabber.Instance.Grab();
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override  void Abort()
        {
           // if (searchThread != null)
           // {
           //     searchThread.Abort();
           // }
        }
    }
}
