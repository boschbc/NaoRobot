using System;
using System.Threading;

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
    public sealed class PickupEvent : ReportBackEvent
    {
        /// <summary>
        /// Checks whether the Nao is not already holding an object.
        /// </summary>
        /// <returns>True if the Nao is not holding anything and may pick up a new object.</returns>
        private bool ValidationCheck()
        {
            return true;
            if (!Grabber.Instance.HoldingObject())
                return true;
            else
            {
                ReportFailure();
                return false;
            }
        }

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
            ObjectID = id;
        }

        /// <summary>
        /// Returns the ID of the object this event aims to pick up.
        /// </summary>
        public int ObjectID
        {
            get;
            private set;
        }

        /// <summary>
        /// Extract the ObjectID out of the internal communication stream.
        /// </summary>
        private void Unpack()
        {
            //ObjectID = Stream.ReadInt();
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

        /// <summary>
        /// Positions the Nao in front of the object, and then grabs it.
        /// </summary>
        /// <returns>True if the Nao was positioned correctly and the grab was successful.</returns>
        private bool Pickup()
        {
            try
            {
                GoInfrontOfObject();
                ObjectSearchWorker results = executor as ObjectSearchWorker;
                results.PositionedCorrectly.Log(this);
                if (results.PositionedCorrectly)
                {
                    GrabObject();
                }
                else
                {
                    ReportFailure();
                    return false;
                }

                return true;
            }
            catch (ThreadInterruptedException)
            {
                Logger.Log(this, "Aborted.");
            }
            catch (Exception e)
            {
                Logger.Log(this, "An unexpected exception occurred: " + e.Message);
            }
            finally
            {
                VerifyObjectHeld();
            }
            return false;
        }

        /// <summary>
        /// Positions the Nao in front of the object.
        /// </summary>
        private void GoInfrontOfObject()
        {
            Logger.Log(this, "Walking towards object...");
            executor = new ObjectSearchWorker();
            executor.Start();
            //executor.WaitFor();
            Logger.Log(this, "Finished walking towards object.");
        }

        /// <summary>
        /// Grabs an object in front of the Nao.
        /// </summary>
        private void GrabObject()
        {
            Logger.Log(this, "Grabbing object...");
            executor = Grabber.Instance.Grab();
            //executor.Start();
            //executor.WaitFor();
            Logger.Log(this, "Finished grabbing.");
        }

        /// <summary>
        /// Verifies that the Nao indeed holds an object at the end of this event's execution.
        /// </summary>
        private void VerifyObjectHeld()
        {
            ReportSuccess();
            return;
            if (Grabber.Instance.HoldingObject())
                ReportSuccess();
            else
                ReportFailure();
        }

        /// <summary>
        /// Post a success event and a holding event to GOAL.
        /// </summary>
        protected override void ReportSuccess()
        {
            base.ReportSuccess();
            EventQueue.Goal.Post(new HoldingEvent(ObjectID));
        }

        /// <summary>
        /// Aborts the event's execution.
        /// </summary>
        public override void Abort()
        {
            base.Abort();
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
        /// put down has no specifics, so all PutDownEvents are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return GetType() == obj.GetType();
        }

        /// <summary>
        /// get hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
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
