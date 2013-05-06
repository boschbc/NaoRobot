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

        public UpdaterThread(int interval_, Action DoWork_)
        {
            DoWork = DoWork_;
            interval = interval_;
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
