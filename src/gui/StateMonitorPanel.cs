using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Timers;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public partial class StateMonitorPanel : UserControl
    {
        private List<IRealtimeField> debugWidgets;
        private System.Timers.Timer updateTimer;

        public StateMonitorPanel()
        {
            InitializeComponent();
            InitializeDebugWidgets();
            updateTimer = new System.Timers.Timer(500);
            updateTimer.Elapsed += new ElapsedEventHandler(updateTimer_Tick);
            //updateTimer.Enabled = true;
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
            Console.WriteLine("Updating stats...");
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
