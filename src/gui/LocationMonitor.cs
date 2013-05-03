using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Aldebaran.Proxies;

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
            PointF location = NaoState.Location;
            float rotation = NaoState.Rotation;
            label.Text = String.Format(Format,
                                    Math.Round(location.X, 2),
                                    Math.Round(location.Y, 2),
                                    Math.Round(rotation, 2));     
        }
    }
}
