using System;
using System.Windows.Forms;
using Naovigate.Util;

namespace Naovigate.GUI.State
{
    /// <summary>
    /// A control that displays the Nao's location in real time.
    /// </summary>
    public sealed partial class LocationMonitor : UserControl, IRealtimeField
    {
        public LocationMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the location display.
        /// </summary>
        private void UpdateLocation()
        {
            locationLabel.Text = String.Format("({0},{1})",
                Math.Round(NaoState.Instance.Location.X, 2),
                Math.Round(NaoState.Instance.Location.Y, 2));
        }

        /// <summary>
        /// Clears the location display.
        /// </summary>
        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (locationLabel.InvokeRequired)
                locationLabel.Invoke(new MethodInvoker(ResetContent));
            else
                locationLabel.Text = "Unknown";
        }

        /// <summary>
        /// Updates the location display.
        /// </summary>
        public void UpdateContent()
        {
            //Avoid cross-thread exception:
            if (locationLabel.InvokeRequired)
                locationLabel.Invoke(new MethodInvoker(UpdateContent));
            else
                if (!locationLabel.IsDisposed)
                    UpdateLocation();
        }
    }
}
