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
        private event Action Done;
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
        /// Any exception that has occurred during execution.
        /// If none occurred, returns null.
        /// </summary>
        public Exception Error
        {
            get;
            protected set;
        }

        /// <summary>
        /// Starts execution in a new thread.
        /// Has no effect if this executor has been previously aborted.
        /// </summary>
        public void Start()
        {
            if (Aborted)
                return;
            //Thread t = new Thread(new ThreadStart(RunInit));
            //t.Name = "ActionExecutor";
            //t.Start();
            Aborted = false;
            Running = true;
            Started = true;
            Run();
            //while (!Started) Thread.Sleep(100);
        }

        /// <summary>
        /// Initializer that invokes the Run() method.
        /// When Run() terminates, all handlers who subscribed via NotifyWhenDone() will be notified.
        /// </summary>
        private void RunInit()
        {
            Aborted = false;
            Running = true;
            Started = true;
            try
            {
                Run();
            }
            catch (Exception e)
            {
                Logger.Log(this, "Got " + e.GetType().Name + " " + e.Message);
                Error = e;
            }
            Running = false;
            if (Done != null)
                Done();
        }

        /// <summary>
        /// This is where sub-classes should perform their work.
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// Will terminate the executor thread at the first possible opportunity.
        /// Has no effect if the thread is already terminated.
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
        /// <exception cref="Exception">An exception was thrown while waiting.</exception>
        public void WaitFor()
        {
            while (!Started)
                Thread.Sleep(100);
            Logger.Log(this, "WaitFor");
            while (Running && Error == null)
                Thread.Sleep(100);
            if (Error != null) throw Error;
            if (Aborted)
            {
                Error = new ThreadInterruptedException("Thread was aborted");
                throw Error;
            }
        }

        /// <summary>
        /// Call the given handler when this ActionExecutor finishes.
        /// </summary>
        /// <param name="handler"></param>
        public void NotifyWhenDone(Action handler)
        {
            Done += handler;
        }
    }
}
