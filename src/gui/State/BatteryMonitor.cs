using System.Windows.Forms;

using Naovigate.Util;

namespace Naovigate.GUI.State
{
    /// <summary>
    /// A control that displays the nao's battery charge in real time.
    /// </summary>
    public sealed partial class BatteryMonitor : UserControl, IRealtimeField
    {
        public BatteryMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the battery charage display to unknown (=0)
        /// </summary>
        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (batteryGauge.InvokeRequired)
                batteryGauge.Invoke(new MethodInvoker(ResetContent));
            else
                batteryGauge.Value = 0;
        }

        /// <summary>
        /// Updates the battery-charge display.
        /// </summary>
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
