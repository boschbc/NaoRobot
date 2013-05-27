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

        /*
         * wait until the current worker is finnished
         */
        public static void WaitFor()
        {
            if(Instance.worker != null)
                Instance.worker.WaitFor();
        }

        /*
         * 
         */
        private Worker DoWork<Worker>(Worker w) where Worker : ActionExecutor
        {
            WaitFor();
            worker = w;
            w.Start();
            return w;
        }

        /*
         * The movement for the grabbing
         */
        public GrabWorker Grab()
        {
            return DoWork(new GrabWorker());
        }
        
        /*
         * put down the object the nao is holding
         */
        public PutDownWorker PutDown()
        {
            return DoWork(new PutDownWorker());
        }        

        /// <summary>
        /// If the grabber is currently grabbing or dropping, abort the operation.
        /// </summary>
        public static void Abort()
        {
            if(instance.worker != null){
                instance.worker.Abort();
            }
        }
    }
}
