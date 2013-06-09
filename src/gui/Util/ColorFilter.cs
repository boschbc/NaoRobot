using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Naovigate.GUI.Util
{
    public sealed partial class ColorFilter : UserControl
    {
        public ColorFilter()
        {
            InitializeComponent();
        }

        public double[] Min
        {
            get { return minRGB.RGB; }
        }

        public double[] Max
        {
            get { return maxRGB.RGB; }
        }
    }
}
