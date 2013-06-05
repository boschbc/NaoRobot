﻿using System;

using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Grabbing;
using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.Event.GoalToNao
{
    /// <summary>
    /// A class representing a PickUp event.
    /// When fired, the Nao will look for a given object ID,
    /// if the object is not visible, posts a failure-event.
    /// Otherwise, walks towards the object and grabs it.
    /// </summary>
    public class PickupEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.Pickup;
        private int id;
        private ActionExecutor executor;

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
            executor = new ObjectSearchThread(ObjectID);
            executor.Start();
            executor.WaitFor();
        }

        /// <summary>
        /// Aborts the event's execution.
        /// </summary>
        public override  void Abort()
        {
            if (executor == null)
                return;
            try
            {
                executor.Abort();
            }
            catch
            {
                EventQueue.Goal.Post(new ErrorEvent());
            }
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Pickup; }
        }
    }
}
