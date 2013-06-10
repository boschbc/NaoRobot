using System;

using Aldebaran.Proxies;

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
        private MotionProxy motion;
        private RobotPostureProxy posture;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Grabber()
        {
            motion = Proxies.GetProxy<MotionProxy>();
            posture = Proxies.GetProxy<RobotPostureProxy>();
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
            Logger.Log(this, "Waiting for grabber...");
            if (worker != null && worker.Running)
            {
                worker.WaitFor();
            }
            Logger.Log(this, "Done waiting.");
        }

        /// <summary>
        /// Prepares the Grabber for execution of given worker.
        /// </summary>
        /// <typeparam name="Worker">The type of given worker.</typeparam>
        /// <param name="w">An ActionExecutor.</param>
        /// <param name="autostart">Controls whether to already invoke Start() on the given worker.</param>
        /// <returns>The worker thread.</returns>
        private Worker CreateWorker<Worker>(Worker w, bool autostart) 
            where Worker : ActionExecutor
        {
            WaitFor();
            worker = w;
            if (autostart)
                w.Start();
            return w;
        }

        /// <summary>
        /// The Nao will attempt to grab the object.
        /// </summary>
        /// <returns>A GrabWorker thread.</returns>
        public GrabWorker Grab()
        {
            return CreateWorker(new GrabWorker(), true);
        }
        
        /// <summary>
        /// The Nao will put down any object it is holding.
        /// Has no effect if the Nao is not holding anything.
        /// </summary>
        /// <returns></returns>
        public virtual PutDownWorker PutDown()
        {
            return CreateWorker(new PutDownWorker(), false);
        }

        /// <summary>
        /// Returns true if the Nao is currently holding an object.
        /// </summary>
        /// <returns>A boolean.</returns>
        public virtual Boolean HoldingObject()
        {
            Logger.Log(this, "HOlding: " + NaoState.Instance.HoldingObject);
            return NaoState.Instance.HoldingObject;
        }

        /// <summary>
        /// If the grabber is currently grabbing or dropping, abort the operation.
        /// </summary>
        public virtual void Abort()
        {
            if (worker != null)
            {
                worker.Abort();
                worker = null;
            }
        }
    }
}
