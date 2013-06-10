using System.Collections.Generic;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI.State
{
    /// <summary>
    /// A control that display several properties of the Nao in real time.
    /// </summary>
    public sealed partial class StateMonitorPanel : UserControl
    {
        private static int DefaultFps = 2;

        private List<IRealtimeField> debugWidgets;
        private int fps;
        private UpdaterThread updater;
        
        /// <summary>
        /// Creates a new instance of this control with the deafult FPS (2).
        /// </summary>
        public StateMonitorPanel()
        {
            fps = DefaultFps;
            Init();
        }

        /// <summary>
        /// Creates a new instance of this control with the given FPS.
        /// </summary>
        /// <param name="fps"></param>
        public StateMonitorPanel(int fps)
        {
            this.fps = fps;
            Init();
        }

        /// <summary>
        /// The refresh interval of this control in ms.
        /// </summary>
        public int Interval
        {
            get { return 1000 / fps; }
        }

        /// <summary>
        /// True when the control is updating in real time.
        /// </summary>
        public bool Active
        {
            get { return updater.Enabled; }
            set { updater.Enabled = value; }
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void Init()
        {
            debugWidgets = new List<IRealtimeField>();
            updater = new UpdaterThread(Interval, UpdateContent);
            InitializeComponent();
            InitializeDebugWidgets();
        }

        /// <summary>
        /// Adds all stat-monitors to this control.
        /// </summary>
        private void InitializeDebugWidgets()
        {
            debugWidgets.Add(locationMonitor);
            debugWidgets.Add(batteryMonitor);
            debugWidgets.Add(temperatureMonitor);
            debugWidgets.Add(rotationMonitor);
        }

        /// <summary>
        /// Updates all displayed fields.
        /// </summary>
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
                rf.UpdateContent();
        }
    }
}
