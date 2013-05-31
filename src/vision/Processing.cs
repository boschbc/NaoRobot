using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;

using Naovigate.Util;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Naovigate.Vision
{
    public class Processing
    {
        private Camera cam;
        private Image<Rgb, Byte> currentImage;
        public Processing(Camera cm)
        {
            cam = cm;
            cam.CalibrateCamera(3);
        }

        public ArrayList DetectObject()
        {
            currentImage = cam.GetImage();
            Image<Gray, Byte> gray = currentImage.Convert<Gray, Byte>();
            return null;   
        }

        public Image<Gray, Byte> EnchancedImage(double[] rgb1, double[] rgb2)
        {
            currentImage = cam.GetImage();
            Rgb p1 = new Rgb(rgb1[0],rgb1[1],rgb1[2]);
            Rgb p2 = new Rgb(rgb2[0], rgb2[1], rgb2[2]);

            Image<Gray, Byte> gray = currentImage.InRange(p1,p2);
            return gray;
        }

        public double ObjectAngle(Image<Rgb,byte> img)
        {
            return 0.0;
        }
        public double ObjectDistance(Image<Rgb, byte> img)
        {
            return 0.0;
        }
    }
}

