using System;

using Aldebaran.Proxies;

using Naovigate.Movement;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.Grabbing
{
    /// <summary>
    /// A class the manages and controls any grabbing/laying procedures of the Nao.
    /// </summary>
    public class Grabber
    {
        private static Grabber instance;
        private MotionProxy motion;
        private RobotPostureProxy posture;
        private Camera camera;
        private Processing processor;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Grabber()
        {
            NaoState.Instance.OnConnect += BuildCameraAndProcessor;
            NaoState.Instance.OnDisconnect += ClearCameraAndProcessor;
            if (NaoState.Instance.Connected)
                BuildCameraAndProcessor(NaoState.Instance.IP.ToString(), NaoState.Instance.Port);
            motion = Proxies.GetProxy<MotionProxy>();
            posture = Proxies.GetProxy<RobotPostureProxy>();
        }

        public void BuildCameraAndProcessor(string ip, int port)
        {
            camera = new Camera("Grabber");
            processor = new Processing(camera);
        }

        public void ClearCameraAndProcessor(string ip, int port)
        {
            camera = null;
            processor = null;
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
            if (Worker != null && Worker.Running)
            {
                try
                {
                    Worker.WaitFor();
                }
                catch(Exception e)
                {
                    Logger.Log(this, "WaitFor failed: "+e);
                }
            }
            Logger.Log(this, "Done waiting.");
        }

        public ActionExecutor Worker
        {
            get;
            private set;
        }

        /// <summary>
        /// Prepares the Grabber for execution of given worker.
        /// </summary>
        /// <typeparam name="Worker">The type of given worker.</typeparam>
        /// <param name="w">An ActionExecutor.</param>
        /// <param name="autostart">Controls whether to already invoke Start() on the given worker.</param>
        /// <returns>The worker thread.</returns>
        private TWorker CreateWorker<TWorker>(TWorker w, bool autostart) 
            where TWorker : ActionExecutor
        {
            WaitFor();
            Worker = w;
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
        public virtual bool HoldingObject()
        {
            //motion.getwa
            //motion.setWalkArmsEnabled(false, false);
            bool holdingObject;
            float rad = (float)(0.25 * Math.PI);
            float accuracy = 0.1f;

            Walk.Instance.Turn(rad);
            Pose.Instance.LookDown();

            holdingObject = processor.ObjectInSight();
            Logger.Log(this, "The Nao is holding an object: " + holdingObject);
            Pose.Instance.LookStraight();
            Walk.Instance.Turn(-rad);

            return holdingObject;
        }

        /// <summary>
        /// If the grabber is currently grabbing or dropping, abort the operation.
        /// </summary>
        public virtual void Abort()
        {
            if (Worker != null)
            {
                Worker.Abort();
                Worker = null;
            }
        }
    }
}
