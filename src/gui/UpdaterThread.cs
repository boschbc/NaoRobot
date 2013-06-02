using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Naovigate.GUI
{
    class UpdaterThread
    {
        private int interval;
        private bool enabled;
        private Action DoWork;

        public UpdaterThread(int interval, Action DoWork)
        {
            this.DoWork = DoWork;
            this.interval = interval;
        }

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

        private void Main()
        {
            while (Enabled)
            {
                DoWork();
                Thread.Sleep(interval);
            }
        }

        public void Start()
        {
            new Thread(new ThreadStart(Main)).Start(); 
        }
    }
}
