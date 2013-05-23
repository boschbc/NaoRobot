using System;
using Naovigate.Communication;
using Naovigate.Grabbing;
using Naovigate.Movement;
using Naovigate.Util;
using Naovigate.Event.NaoToGoal;
namespace Naovigate.Event.GoalToNao
{
    /*
     * @param ID
     * Pick up the object with id ID
     */
    public class PickupEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Pickup;
        private int id;
        private ObjectSearchThread searchThread;

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
            NaoEvent statusEvent = new SuccessEvent(code); ;
            try
            {
                // go to the object first
                searchThread = Walk.Instance.WalkTowardsObject(0, 0, 0);

                // grab the object
                Grabber.Instance.Grab();
            }
            catch
            {
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override  void Abort()
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
        }
    }
}
