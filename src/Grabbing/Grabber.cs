using System;

using Aldebaran.Proxies;

using Naovigate.Communication;
using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.Grabbing
{
    /// <summary>
    /// A class the manages and controls any grabbing/laying procedures of the Nao.
    /// </summary>
    public class Grabber
    {
        private static Grabber instance;
        private ActionExecutor worker;
        MotionProxy motion;
        RobotPostureProxy posture;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Grabber()
        {
            motion = NaoState.Instance.MotionProxy;
            posture = NaoState.Instance.PostureProxy;
            instance = this;
        }

        /// <summary>
        /// The motion proxy used by this class.
        /// </summary>
        public MotionProxy Motion
        {
            get { return motion; }
        }

        /// <summary>
        /// The posture proxy used by this class.
        /// </summary>
        public RobotPostureProxy Posture
        {
            get { return posture; }
        }
        
        /// <summary>
        /// This singleton's instance.
        /// </summary>
        public static Grabber Instance
        {
            get
            {
                return instance == null ? instance = new Grabber() : instance;
            }
            set { instance = value; }
        }

        /// <summary>
        /// Blocks the current thread until the grabbing process has been completed.
        /// </summary>
        public void WaitFor()
        {
            Logger.Log(typeof(Grabber), "Waiting for grabber.");
            if (worker != null && worker.Running)
            {
                worker.WaitFor();
            }
            Logger.Log(typeof(Grabber), "Done waiting.");
        }

        /// <summary>
        /// Someone needs to document this method.
        /// </summary>
        /// <typeparam name="Worker"></typeparam>
        /// <param name="w"></param>
        /// <returns></returns>
        private Worker DoWork<Worker>(Worker w) where Worker : ActionExecutor
        {
            WaitFor();
            worker = w;
            w.Start();
            return w;
        }

        /// <summary>
        /// The Nao will attempt to grab the object.
        /// </summary>
        /// <returns>A GrabWorker thread.</returns>
        public GrabWorker Grab()
        {
            return DoWork(new GrabWorker());
        }
        
        /// <summary>
        /// The Nao will put down any object it is holding.
        /// Has no effect if the Nao is not holding anything.
        /// </summary>
        /// <returns></returns>
        public virtual PutDownWorker PutDown()
        {
            return DoWork(new PutDownWorker());
        }

        /// <summary>
        /// Returns true if the Nao is currently holding an object.
        /// </summary>
        /// <returns>A boolean.</returns>
        public virtual Boolean HoldingObject()
        {
            //TODO 
            return true;
        }

        /// <summary>
        /// If the grabber is currently grabbing or dropping, abort the operation.
        /// </summary>
        public virtual void Abort()
        {
            if (worker != null)
            {
                worker.Abort();
            }
        }
    }
}
