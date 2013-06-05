using System;
using System.Threading;

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
            if (ValidationCheck())
                if (Pickup())
                    VerifyObjectHeld();
        }

        private bool ValidationCheck()
        {
            if (!Grabber.Instance.HoldingObject())
                return true;
            else
            {
                ReportFailure();
                return false;
            }
        }

        private bool Pickup()
        {
            try
            {
                GoInfrontOfObject();
                ObjectPickupThread results = executor as ObjectPickupThread;
                if (!results.ObjectFound)
                {
                    ReportFailure();
                    return false;
                }
                else
                    GrabObject();
               
                return true;
            }
            catch (ThreadInterruptedException)
            {
                Logger.Log(this, "Aborted.");
                VerifyObjectHeld();
            }
            catch (Exception e)
            {
                Logger.Log(this, "An unexpected exception occurred: " + e.Message);
                VerifyObjectHeld();
            }
            return false;
        }

        private void GoInfrontOfObject()
        {
            executor = new ObjectPickupThread(ObjectID);
            executor.Start();
            executor.WaitFor();
        }

        private void GrabObject()
        {
            executor = Grabber.Instance.Grab();
            executor.Start();
            executor.WaitFor();
        }

        private void VerifyObjectHeld()
        {
            ObjectPickupThread results = executor as ObjectPickupThread;
            if (results.ObjectFound && Grabber.Instance.HoldingObject())
                ReportSuccess();
            else
                ReportFailure();
        }

        /// <summary>
        /// Aborts the event's execution.
        /// </summary>
        public override void Abort()
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
    }
}
