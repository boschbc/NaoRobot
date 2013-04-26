using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Aldebaran.Proxies;

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
            UpdateContent();
        }

        public void UpdateContent()
        {
            MotionProxy motion = NaoState.GetMotionProxy();
            try
            {
                List<float> vector = motion.getRobotPosition(false);
                label.Text = String.Format(Format, vector[0], vector[1], vector[2]);
            }
            catch
            {
                label.Text = DefaultText;
            }
        }
    }
}
