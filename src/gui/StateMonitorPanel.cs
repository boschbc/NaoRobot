using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

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
        }

        /**
         * Update the debug data displayed in all debug fiels.
         **/
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            foreach (IRealtimeField rf in debugWidgets)
            {
                rf.UpdateContent();
            }
        }
    }
}
