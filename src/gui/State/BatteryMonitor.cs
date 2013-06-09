﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Util;
using Naovigate.Communication;

namespace Naovigate.GUI.State
{
    public sealed partial class BatteryMonitor : UserControl, IRealtimeField
    {
        public BatteryMonitor()
        {
            InitializeComponent();
        }

        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (batteryGauge.InvokeRequired)
                batteryGauge.Invoke(new MethodInvoker(ResetContent));
            else
                batteryGauge.Value = 0;
        }

        public void UpdateContent()
        {
            //Avoid cross-thread exception:
            if (batteryGauge.InvokeRequired)
                batteryGauge.Invoke(new MethodInvoker(UpdateContent));
            else
                batteryGauge.Value = NaoState.Instance.BatteryPercentageLeft;
        }
    }
}
