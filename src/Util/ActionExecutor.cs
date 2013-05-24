using System;
using System.Collections.Generic;
using System.Threading;

namespace Naovigate.Util
{
    public abstract class ActionExecutor
    {
        protected bool running;

        public bool Running
        {
            get { return running; }
        }

        public void Abort()
        {
            running = false;
        }

        public void Call(Action a)
        {
            if (running) a.Invoke();
            
        }

        /*
         * Start this Executor.
         */
        public void Start()
        {
            Thread t = new Thread(new ThreadStart(Run));
            running = true;
            t.Start();
        }

        public abstract void Run();
    }
}
