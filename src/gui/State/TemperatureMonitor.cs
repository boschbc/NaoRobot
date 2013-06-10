using System;
using System.Windows.Forms;

using Naovigate.Util;

namespace Naovigate.GUI.State
{
    /// <summary>
    /// A control that monitors the Nao's temperature in real time.
    /// </summary>
    public sealed partial class TemperatureMonitor : UserControl, IRealtimeField
    {
        private static string Format = "{0}°C";

        public TemperatureMonitor()
        {
            InitializeComponent();
            SetTemperatureUnknown();
        }

        /// <summary>
        /// Sets the temperature to 'unknown'.
        /// </summary>
        private void SetTemperatureUnknown()
        {
            labelAlert.Text = "Unknown";
            labelAlert.ForeColor = System.Drawing.Color.Black;
        }

        /// <summary>
        /// Sets the temperature level to OK.
        /// </summary>
        private void SetTemperatureOK()
        {
            labelAlert.ForeColor = System.Drawing.Color.Green;
        }

        /// <summary>
        /// Sets the temperature level to hot.
        /// </summary>
        private void SetTemperatureHot()
        {
            labelAlert.ForeColor = System.Drawing.Color.Red;
        }

        /// <summary>
        /// Clears the temperature display.
        /// </summary>
        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (labelAlert.InvokeRequired)
                labelAlert.Invoke(new MethodInvoker(ResetContent));
            else
                SetTemperatureUnknown();
        }

        /// <summary>
        /// Updates the temperature display.
        /// </summary>
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
