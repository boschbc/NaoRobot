using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI.State
{
    public partial class StateMonitorPanel : UserControl
    {
        private static int DefaultFps = 2;

        private List<IRealtimeField> debugWidgets;
        private int fps;
        private UpdaterThread updater;
        
        public StateMonitorPanel()
        {
            fps = DefaultFps;
            Init();
        }

        public StateMonitorPanel(int fps)
        {
            this.fps = fps;
            Init();
        }

        private void Init()
        {
            updater = new UpdaterThread(Interval, UpdateContent);
            InitializeComponent();
            InitializeDebugWidgets();
        }

        private void InitializeDebugWidgets()
        {
            debugWidgets = new List<IRealtimeField>();
            debugWidgets.Add(locationMonitor);
            debugWidgets.Add(batteryMonitor);
            debugWidgets.Add(temperatureMonitor);
            debugWidgets.Add(rotationMonitor);
        }

        public int Interval
        {
            get { return 1000 / fps; }
        }

        public bool Active
        {
            get { return updater.Enabled; }
            set { updater.Enabled = value; }
        }
        
        private void UpdateContent()
        {
            if (!NaoState.Instance.Connected)
            {
                foreach (IRealtimeField rf in debugWidgets)
                    rf.ResetContent();
                return;
            }
            else if (NaoState.Instance.OutOfDate(Interval))
            {
                try
                {
                    NaoState.Instance.Update();
                }
                catch (UnavailableConnectionException)
                {
                    Logger.Log(this, "Failed to update. Connection unavailable.");
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
