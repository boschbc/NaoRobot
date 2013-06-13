using System;
using System.Threading;

namespace Naovigate.Util
{
    /// <summary>
    /// A wrapper class for some work that needs to be executed asynchronously.
    /// It provides any extending classes with the tools to be able to halt the executed work as fast as possible.
    /// </summary>
    public abstract class ActionExecutor
    {
        /// <summary>
        /// True if execution is ongoing.
        /// </summary>
        public bool Running
        {
            get;
            private set;
        }

        public bool Started
        {
            get;
            private set;
        }

        /// <summary>
        /// True if this executor was aborted.
        /// </summary>
        public bool Aborted
        {
            get;
            private set;
        }

        /// <summary>
        /// Starts execution.
        /// Has no effect if this executor has been previously aborted.
        /// </summary>
        public void Start()
        {
            if (Aborted)
                return;
            Aborted = false;
            Running = true;
            Started = true;
            try
            {
                Run();
            }
            finally
            {
                Running = false;
            }
        }

        /// <summary>
        /// This is where sub-classes should perform their work.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Will terminate the executor at the first possible opportunity.
        /// Has no effect if already done or aborted.
        /// </summary>
        public void Abort()
        {
            Running = false;
            Aborted = true;
        }

        /// <summary>
        /// Calls an Action.
        /// If the executor is not currently running,
        /// throws a new instance of ThreadInterruptedException.
        /// </summary>
        /// <param name="a">The action to be invoked.</param>
        public void Call(Action a)
        {
            if (Running) a.Invoke();
            else throw new ThreadInterruptedException("Thread was aborted.");
        }

        /// <summary>
        /// Block the current thread until this executor finished running.
        /// </summary>
        public void WaitFor()
        {
            Logger.Log(this, "WaitForStart");
            while (!Started)
                Thread.Sleep(100);
            Logger.Log(this, "WaitForStop");
            while (Running)
                Thread.Sleep(100);
        }
    }
}
