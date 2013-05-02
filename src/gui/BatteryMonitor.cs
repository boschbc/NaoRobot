﻿using System;
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
            try
            {
                NaoState.Update();
            }
            catch (UnavailableConnectionException e)
            {
                Console.WriteLine("Caught exception: " + e.Message);
                return;
            }
            batteryGauge.Value = NaoState.BatteryPercentageLeft;
        }
    }
}
