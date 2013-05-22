using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Util;
using Naovigate.Communication;

namespace Naovigate.GUI
{
    public partial class BatteryMonitor : UserControl, IRealtimeField
    {
        public BatteryMonitor()
        {
            InitializeComponent();
        }

        public void UpdateContent()
        {
            //Avoid cross-thread exception:
            if (batteryGauge.InvokeRequired)
            {
                batteryGauge.Invoke(new MethodInvoker(UpdateContent));
            }
            batteryGauge.Value = NaoState.Instance.BatteryPercentageLeft;
        }
    }
}
