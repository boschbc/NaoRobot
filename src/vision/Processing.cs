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
        }

        public ArrayList DetectObject()
        {
            currentImage = cam.GetImage();
            Image<Gray, Byte> gray = currentImage.Convert<Gray, Byte>();
            return null;   
        }

        public Image<Gray, Byte> EnchancedImage()
        {
            currentImage = cam.GetImage();
            Rgb p1 = new Rgb(0.0, 120.0, 0.0);
            Rgb p2 = new Rgb(255.0, 170.0, 255.0);
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

