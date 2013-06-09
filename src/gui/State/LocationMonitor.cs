using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Naovigate.Util;

namespace Naovigate.GUI.State
{
    public sealed partial class LocationMonitor : UserControl, IRealtimeField
    {
        public LocationMonitor()
        {
            InitializeComponent();
        }

        private void UpdateLocation()
        {
            locationLabel.Text = String.Format("({0},{1})",
                Math.Round(NaoState.Instance.Location.X, 2),
                Math.Round(NaoState.Instance.Location.Y, 2));
        }

        public void ResetContent()
        {
            //Avoid cross-thread exception:
            if (locationLabel.InvokeRequired)
                locationLabel.Invoke(new MethodInvoker(ResetContent));
            else
                locationLabel.Text = "Unknown";
        }

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
