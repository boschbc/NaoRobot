using System;
using System.Collections.Generic;
using System.Threading;

namespace Naovigate.Util
{
    public abstract class ActionExecutor
    {
        protected bool running;
        protected Exception e = null;
        public bool Running
        {
            get { return running; }
        }

        /*
         * the Exception that occured in this executor.
         * if there is no Exception, null will be returned.
         */
        public Exception Error
        {
            get { return e; }
            protected set { 
                running = false; 
                e = value; 
            }
        }

        /*
         * wait until this ActionExecutor has finnished.
         * throws an error if one occured in the ActionExecutor
         */
        public void WaitFor()
        {
            while(Running) Thread.Sleep(100);
            if (Error != null) throw Error;
        }

        public void Abort()
        {
            running = false;
        }

        /*
         * call a method if we are still running.
         * if we are not running anymore, ignore the method,
         * and set the error to a ThreadInterruptedException.
         */
        public void Call(Action a)
        {
            if (running && Error == null) a.Invoke();
            else if (e == null) e = new ThreadInterruptedException();
        }

        /*
         * Start this Executor.
         */
        public void Start()
        {
            Thread t = new Thread(new ThreadStart(RunInit));
            running = true;
            t.Start();
        }

        private void RunInit()
        {
            Run();
            running = false;
        }

        public abstract void Run();
    }
}
