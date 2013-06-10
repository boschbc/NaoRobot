using System;
using System.Windows.Forms;

using Naovigate.Util;

namespace Naovigate.GUI.State
{
    /// <summary>
    /// A control that displays the Nao's rotation in real time.
    /// </summary>
    public sealed partial class RotationMonitor : UserControl, IRealtimeField
    {
        public RotationMonitor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the rotation display.
        /// </summary>
        private void UpdateRotation()
        {
            rotationLabel.Text = String.Format("{0}°",
                Math.Round(NaoState.Instance.Rotation, 3).ToString("0.000"));
        }

        /// <summary>
        /// Clears the rotation display.
        /// </summary>
        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (rotationLabel.InvokeRequired)
                rotationLabel.Invoke(new MethodInvoker(ResetContent));
            else
                rotationLabel.Text = "Unknown";
        }

        /// <summary>
        /// Updates the rotation display.
        /// </summary>
        public void UpdateContent()
        {
            //Avoid cross-thread exception:
            if (rotationLabel.InvokeRequired)
                rotationLabel.Invoke(new MethodInvoker(UpdateContent));
            else
                UpdateRotation();
        }
    }
}
