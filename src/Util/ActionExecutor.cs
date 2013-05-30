using System;
using System.Collections.Generic;
using System.Threading;

namespace Naovigate.Util
{
    /// <summary>
    /// A wrapper class for some work that needs to be executed asynchronously.
    /// It provides any extending classes with the tools to be able to halt the executed work as fast as possible.
    /// </summary>
    public abstract class ActionExecutor
    {
        protected bool running;
        protected bool aborted;
        protected Exception e = null;

        /// <summary>
        /// True if execution is ongoing.
        /// </summary>
        public bool Running
        {
            get { return running; }
        }

        /// <summary>
        /// True if this executor was aborted.
        /// </summary>
        public bool Aborted
        {
            get { return aborted; }
        }

        /// <summary>
        /// Any exception that has occurred during execution.
        /// If none occurred, returns null.
        /// </summary>
        public Exception Error
        {
            get { return e; }
            protected set
            { 
                e = value; 
            }
        }

        /// <summary>
        /// Block the current thread until this executor finished running.
        /// </summary>
        /// <exception cref="Exception">An exception was thrown while waiting.</exception>
        public void WaitFor()
        {
            while (Running && Error == null)
                Thread.Sleep(100);
            if (Error != null)
                throw Error;
        }

        /// <summary>
        /// Will terminate the executor thread at the first possible opportunity.
        /// Has no effect if the thread is already terminated.
        /// </summary>
        public void Abort()
        {
            if (running)
            {
                running = false;
                aborted = true;
            }
        }

        /// <summary>
        /// Calls an Action.
        /// If the executor is not currently running, sets the Error property to a new instance of ThreadInterruptedException.
        /// </summary>
        /// <param name="a">The action to be invoked.</param>
        public void Call(Action a)
        {
            if (Running)
                a.Invoke();
            //else if (Error == null) Error = new ThreadInterruptedException();
        }

        /// <summary>
        /// Starts execution in a new thread.
        /// </summary>
        public void Start()
        {
            Thread t = new Thread(new ThreadStart(RunInit));
            running = true;
            aborted = false;
            t.Start();
        }

        /// <summary>
        /// Initialiser that invokes the Run() method.
        /// </summary>
        private void RunInit()
        {
            try
            {
                Run();
            }
            catch (Exception e)
            {
                Error = e;
            }
            running = false;
        }

        /// <summary>
        /// This is where sub-classes should perform their work.
        /// </summary>
        public abstract void Run();
    }
}
