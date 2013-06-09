using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Util;

namespace Naovigate.GUI.State
{
    public sealed partial class TemperatureMonitor : UserControl, IRealtimeField
    {
        private static string Format = "{0}°C";

        public TemperatureMonitor()
        {
            InitializeComponent();
            SetTemperatureUnknown();
        }

        private void SetTemperatureUnknown()
        {
            labelAlert.Text = "Unknown";
            labelAlert.ForeColor = System.Drawing.Color.Black;
        }

        private void SetTemperatureOK()
        {
            labelAlert.ForeColor = System.Drawing.Color.Green;
        }

        private void SetTemperatureHot()
        {
            labelAlert.ForeColor = System.Drawing.Color.Red;
        }

        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (labelAlert.InvokeRequired)
                labelAlert.Invoke(new MethodInvoker(ResetContent));
            else
                SetTemperatureUnknown();
        }

        public void UpdateContent()
        {
            //Avoid cross-thread exception:
            if (labelAlert.InvokeRequired)
            {
                labelAlert.Invoke(new MethodInvoker(UpdateContent));
                return;
            }
            
            labelAlert.Text = String.Format(Format, NaoState.Instance.Temperature.ToString());
            if (NaoState.Instance.Temperature > 40)
                SetTemperatureHot();
            else
                SetTemperatureOK();
            
        }
    }
}
