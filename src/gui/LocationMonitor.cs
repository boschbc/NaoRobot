using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public partial class LocationMonitor : UserControl, IRealtimeField
    {
        private static string Format = "Location: ({0}, {1})\nAngle: {2}";
        private static string DefaultText = "Location: Unknown\nAngle: Unknown"; 


        public LocationMonitor()
        {
            InitializeComponent();
            label.Text = DefaultText;
        }

        public void UpdateContent()
        {
            //Avoid cross-thread exception:
            if (label.InvokeRequired)
            {
                label.Invoke(new MethodInvoker(UpdateContent));
            }
            else
            {
                PointF location = NaoState.Instance.Location;
                float rotation = NaoState.Instance.Rotation;
                label.Text = String.Format(Format,
                                    Math.Round(location.X, 2),
                                    Math.Round(location.Y, 2),
                                    Math.Round(rotation, 2));
            }
        }
    }
}
