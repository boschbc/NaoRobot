using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public partial class StateMonitorPanel : UserControl
    {
        private static int DefaultFps = 2;

        private List<IRealtimeField> debugWidgets;
        private int fps;
        private UpdaterThread worker;
        
        public StateMonitorPanel()
        {
            fps = DefaultFps;
            Init();
        }

        public StateMonitorPanel(int fps_)
        {
            fps = fps_;
            Init();
        }

        private void Init()
        {
            worker = new UpdaterThread(Interval, UpdateContent);
            InitializeComponent();
            InitializeDebugWidgets();
            worker.Enabled = true;
        }

        private void InitializeDebugWidgets()
        {
            debugWidgets = new List<IRealtimeField>();
            debugWidgets.Add(locationMonitor);
            debugWidgets.Add(batteryMonitor);
            debugWidgets.Add(temperatureMonitor);
        }

        public int Interval
        {
            get { return 1000 / fps; }
        }

        /*
         * Stops updating this component.
         */
        public void StopUpdate()
        {
            worker.Enabled = false;
        }

        /*
         * Update the debug data displayed in all debug fiels.
         */
        private void UpdateContent()
        {
            if (!NaoState.Connected)
                return;
            else if (NaoState.OutOfDate(Interval))
            {
                try
                {
                    NaoState.Update();
                }
                catch (UnavailableConnectionException except)
                {
                    Console.WriteLine("Caught exception: " + except.Message);
                    return;
                }
            }
            foreach (IRealtimeField rf in debugWidgets)
            {
                rf.UpdateContent();
            }
        }
    }
}
