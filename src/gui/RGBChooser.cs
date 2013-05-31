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

        public float[] GetMinRGB()
        {
            return new float[3] { RedMin.Value,
                                  GreenMin.Value,
                                  BlueMin.Value };
        }

        public float[] GetMaxRGB()
        {
            return new float[3] { RedMax.Value,
                                  GreenMax.Value,
                                  BlueMax.Value };
        }
    }
}
