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
        private Camera cm;
        private Image<Rgb, Byte> currentImage;
        public Processing(String ip)
        {
            cm = new Camera(ip);
        }

        public ArrayList DetectObject()
        {
            currentImage = cm.GetImage();
            Image<Gray,Byte> gray = currentImage.Convert<Gray,Byte>();
            return null;
            
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

