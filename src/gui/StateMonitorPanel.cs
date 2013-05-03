using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public partial class StateMonitorPanel : UserControl
    {
        private List<IRealtimeField> debugWidgets;

        public StateMonitorPanel()
        {
            InitializeComponent();
            InitializeDebugWidgets();
        }

        private void InitializeDebugWidgets()
        {
            debugWidgets = new List<IRealtimeField>();
            debugWidgets.Add(locationMonitor);
            debugWidgets.Add(batteryMonitor);
        }

        /**
         * Update the debug data displayed in all debug fiels.
         **/
        private void updateTimer_Tick(object sender, EventArgs e)
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

            foreach (IRealtimeField rf in debugWidgets)
            {
                rf.UpdateContent();
            }
        }
    }
}
