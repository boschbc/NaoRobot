using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Util;

namespace Naovigate.GUI.State
{
    public partial class RotationMonitor : UserControl, IRealtimeField
    {
        public RotationMonitor()
        {
            InitializeComponent();
        }

        private void UpdateRotation()
        {
            rotationLabel.Text = String.Format("{0}°",
                NaoState.Instance.Rotation);
        }

        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (rotationLabel.InvokeRequired)
                rotationLabel.Invoke(new MethodInvoker(ResetContent));
            else
                rotationLabel.Text = "Unknown";
        }

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
