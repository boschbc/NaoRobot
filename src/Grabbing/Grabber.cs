using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Aldebaran.Proxies;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Grabbing
{
    /// <description>Class that handles the 'grab' command. Grab an element right in front of the robot</description>    class Grabber
    public class Grabber
    {
        private static Grabber instance;
        private ActionExecutor worker;
        MotionProxy motion;
        RobotPostureProxy posture;
        
        /*
         * Constructor
         */
        public Grabber()
        {
            motion = NaoState.Instance.MotionProxy;
            posture = NaoState.Instance.PostureProxy;
            instance = this;
        }

        public MotionProxy Motion
        {
            get { return motion; }
        }

        public RobotPostureProxy Posture
        {
            get { return posture; }
        }
        
        public static Grabber Instance
        {
            get
            {
                return instance == null ? instance = new Grabber() : instance;
            }
            set { instance = value; }
        }

        public static void WaitFor()
        {
            if(Instance.worker != null)
                while(Instance.worker.Running) System.Threading.Thread.Sleep(100);
        }

        /*
         * The movemont for the grabbing
         */
        public GrabWorker Grab()
        {
            WaitFor();
            GrabWorker w = new GrabWorker();
            worker = w;
            w.Start();
            return w;
        }
        /*
         * put down the object the nao is holding
         */
        public PutDownWorker PutDown()
        {
            WaitFor();
            PutDownWorker w = new PutDownWorker();
            worker = w;
            w.Start();
            return w;
        }        

        /// <summary>
        /// If the grabber is currently grabbing or dropping, abort the operation.
        /// </summary>
        public static void Abort()
        {
            if(Grabber.instance.worker != null){
                Grabber.instance.worker.Abort();
            }
        }
    }
}
