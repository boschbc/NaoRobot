using System;
using System.Threading;

namespace Naovigate.GUI
{
    /// <summary>
    /// A class that calls a certain method in equal intervals.
    /// </summary>
    public sealed class UpdaterThread
    {
        private static int updaterCount;
        private int interval;
        private bool enabled;
        private Action DoWork;

        /// <summary>
        /// Creates a new instance of this class with given interval and method.
        /// </summary>
        /// <param name="interval">An integer specifying time in ms.</param>
        /// <param name="DoWork">A method that will be called in equal intervals.</param>
        public UpdaterThread(int interval, Action DoWork)
        {
            this.DoWork = DoWork;
            this.interval = interval;
        }

        /// <summary>
        /// True when the thread is calling the method in given intervals.
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set 
            { 
                enabled = value;
                if (enabled)
                    Start();
            }
        }

        /// <summary>
        /// Runrs the thread until it becmoes disabled.
        /// </summary>
        private void Main()
        {
            while (Enabled)
            {
                DoWork();
                Thread.Sleep(interval);
            }
        }

        /// <summary>
        /// Start the class's main thread in another thread.
        /// </summary>
        public void Start()
        {
            Thread t = new Thread(new ThreadStart(Main));
            t.Name = NextThreadName();
            t.Start(); 
        }

        /// <summary>
        /// Names a thread based on its time of creation.
        /// </summary>
        /// <returns>A string containing a thread name.</returns>
        private static string NextThreadName()
        {
            return "UpdaterThread" + ++updaterCount;
        }
    }
}
