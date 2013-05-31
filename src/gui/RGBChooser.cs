using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Naovigate.GUI
{
    public partial class RGBChooser : UserControl
    {
        public RGBChooser()
        {
            InitializeComponent();
        }

        public double GetTrack(TrackBar track)
        {
            if (track.InvokeRequired)
            {
                track.Invoke(new MethodInvoker(() => GetTrack(track)));
            }
            else
                return track.Value;
            return default(double);
        }

        public double[] GetMinRGB()
        {
            return new double[3] { GetTrack(RedMin),
                                  GetTrack(GreenMin),
                                  GetTrack(BlueMin) };
        }

        public double[] GetMaxRGB()
        {
            return new double[3] { GetTrack(RedMax),
                                   GetTrack(GreenMax),
                                   GetTrack(BlueMax) };
        }
    }
}
