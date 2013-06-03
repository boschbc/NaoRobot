using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;
using Naovigate.Vision;

namespace Naovigate.GUI.LiveCamera
{
    public partial class CameraEnhancerPanel : UserControl
    {
        private Camera target;

        public CameraEnhancerPanel()
        {
            InitializeComponent();
        }

        public Camera Target
        {
            get { return target; }
            set { target = value; }
        }

        public double[] Min
        {
            get { return minRGB.RGB; }
        }

        public double[] Max
        {
            get { return maxRGB.RGB; }
        }

        public Image Enhance()
        {
            Naovigate.Util.Logger.Log();
            if (Target == null)
                return null;
            Processing ps = new Processing(Target);
            Image<Rgb, Byte> image = Target.GetImage();
            Image<Hsv, Byte> enhanced = ps.EnchancedImage(Min, Max);
            return enhanced.ToBitmap(image.Width, image.Height);
        }
    }
}
