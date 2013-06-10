using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

using Naovigate.Vision;

namespace Naovigate.GUI.LiveCamera
{
    /// <summary>
    /// A control that allows the user to specify thresholding parameters for a video-feed.
    /// </summary>
    internal sealed partial class CameraEnhancerPanel : UserControl
    {
        private Camera target;

        public CameraEnhancerPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The target camera to manipulate.
        /// </summary>
        public Camera Target
        {
            get { return target; }
            set { target = value; }
        }

        /// <summary>
        /// The selected threshold minumum bounds.
        /// </summary>
        public double[] Min
        {
            get { return minRGB.RGB; }
        }

        /// <summary>
        /// The selected threshold maximum bounds.
        /// </summary>
        public double[] Max
        {
            get { return maxRGB.RGB; }
        }

        /// <summary>
        /// Enhances the target camera's image using the selected threshold bounds.
        /// </summary>
        /// <returns></returns>
        public Image Enhance()
        {
            if (Target == null)
                return null;
            Processing ps = new Processing(Target);
            Image<Rgb, Byte> image = Target.GetImage();
            Image<Gray, Byte> enhanced = ps.EnchancedImage(Min, Max);
            return enhanced.ToBitmap(image.Width, image.Height);
        }
    }
}
