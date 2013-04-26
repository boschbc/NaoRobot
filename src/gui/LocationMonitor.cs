using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Aldebaran.Proxies;

namespace Naovigate.gui
{
    public partial class LocationMonitor : UserControl
    {
        private static string Format = "Location: ({0}, {1})\nAngle: {2}";
        private static string DefaultText = "Location: Unknown\nAngle: Unknown"; 

        private MotionProxy proxy;

        public LocationMonitor(string ip, int port)
        {
            InitializeComponent();
            proxy = new MotionProxy(ip, port);
            UpdateContent();
        }

        public void UpdateContent()
        {
            try
            {
                List<float> vector = proxy.getRobotPosition(false);
                label.Text = String.Format(Format, vector[0], vector[1], vector[2]);
            }
            catch
            {
                label.Text = DefaultText;
            }
        }
    }
}
