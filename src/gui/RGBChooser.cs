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
        private double[] rgbMin;
        private double[] rgbMax;
        public RGBChooser()
        {
            InitializeComponent();
            rgbMin = new double[3];
            rgbMax = new double[3];
            rgbMin[0] = RedMin.Value;
            rgbMin[1] = GreenMin.Value;
            rgbMin[2] = BlueMin.Value;

            rgbMax[0] = RedMax.Value;
            rgbMax[1] = GreenMax.Value;
            rgbMax[2] = BlueMax.Value;
        }



        public double[] GetMinRGB()
        {
            return rgbMin;
        }

        public double[] GetMaxRGB()
        {
            return rgbMax;
        }

        private void RedMin_Scroll(object sender, EventArgs e)
        {
            rgbMin[0] = RedMin.Value;
        }

        private void RedMax_Scroll(object sender, EventArgs e)
        {
            rgbMax[0] = RedMax.Value;
        }

        private void GreenMin_Scroll(object sender, EventArgs e)
        {
            rgbMin[1] = GreenMin.Value;
        }

        private void GreenMax_Scroll(object sender, EventArgs e)
        {
            rgbMax[1] = GreenMax.Value;
        }

        private void BlueMin_Scroll(object sender, EventArgs e)
        {
            rgbMin[2] = BlueMin.Value;
        }

        private void BlueMax_Scroll(object sender, EventArgs e)
        {
            rgbMax[2] = BlueMax.Value;
        }
    }
}
