using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// A class representing a PickUp event as specified in the API.
    /// </summary>
    public class PickupEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Pickup;
        private int id;
        private ObjectSearchThread searchThread;

        /// <summary>
        /// Creates a new pickup event.
        /// </summary>
        public PickupEvent()
        {
            Unpack();
        }

        /// <summary>
        /// Explicit constructor.
        /// </summary>
        /// <param name="id">Objectd ID</param>
        public PickupEvent(int id)
        {
            this.id = id;
        }

        /// <summary>
        /// Returns the ID of the object this event aims to pick up.
        /// </summary>
        public int ObjectID
        {
            get { return id; }
        }

        /// <summary>
        /// Extract the ObjectID out of the internal communication stream.
        /// </summary>
        private void Unpack()
        {
            id = stream.ReadInt();
        }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            NaoEvent statusEvent = new SuccessEvent(code); ;
            try
            {
                // go to the object first
                searchThread = Walk.Instance.WalkTowardsObject(0, id, 0);
                searchThread.WaitFor();

                // grab the object
                Grabber.Instance.Grab();
            }
            catch
            {
                statusEvent = new FailureEvent(code);
            }
            EventQueue.Goal.Post(statusEvent);
        }

        /// <summary>
        /// Aborts the event's execution.
        /// </summary>
        public override  void Abort()
        {
            if (searchThread == null)
                return;
            try
            {
                searchThread.Abort();
            }
            catch
            {
                EventQueue.Goal.Post(new ErrorEvent());
            }
        }
    }
}
