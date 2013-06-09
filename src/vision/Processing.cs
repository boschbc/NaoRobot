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
    internal sealed class Processing : IDisposable
    {
        public static bool closeEnough(Rectangle rect)
        {
            return rect.Width >= 135;
        }

        //constructs a arraylist of data from the objectRectangle
        public static ArrayList ObjectData(Rectangle objectRectangle)
        {
            ArrayList ret = new ArrayList();
            ret.Add(objectRectangle.Left);
            ret.Add(objectRectangle.Right);
            return ret;
        }

        //returns the biggest rectangle object
        public static Rectangle biggestRectangle(List<Rectangle> rectangles)
        {
            Rectangle max = new Rectangle(0, 0, 0, 0);
            foreach (Rectangle rect in rectangles)
            {
                if (max.Width < rect.Width)
                {
                    max = rect;
                }
            }
            return max;
        }

        private Camera cam;
        private Image<Rgb, Byte> currentImage;
        private List<Hsv> colors;
        ObjectRecogniser rec;
        List<Rectangle> rectangles;
        double naoFactor = 2;

        public Processing(Camera camera)
        {
            cam = camera;
            cam.Subscribe();
            rec = ObjectRecogniser.Instance;
            Init();
        }

        public void Init()
        {
            colors = new List<Hsv>();
            Hsv redObjectMIn = new Hsv(255, 255, 255);
            Hsv redObjectMax = new Hsv ( 255.0, 255.0, 255.0);
            Hsv greenObjectMin = new Hsv ( 80, 150, 43 );
            Hsv greenObjectMax = new Hsv ( 116, 199, 180 );
            Hsv blueObjectMin = new Hsv (255, 255, 255);
            Hsv blueObjectMax = new Hsv (255, 255, 255);
            colors.Add(redObjectMIn);
            colors.Add(redObjectMax);
            colors.Add(greenObjectMin);
            colors.Add(greenObjectMax);
            colors.Add(blueObjectMin);
            colors.Add(blueObjectMax);
        }
        //checks of there is a object in sight and then returns a array with distance and angle
        public Rectangle DetectObject()
        {
            currentImage = cam.GetImage();
            Image<Hsv, Byte> hsvImg = currentImage.Convert<Hsv, Byte>();
            return SearchForObjects(hsvImg);
        }

        

        public float calculateTheta(Rectangle rect)
        {
            int middle = rect.Left + rect.Right - currentImage.Width;
            float ret = 0;
            if (Math.Abs(middle) > 50)
            {
                ret =  middle > 0 ? -0.1F : 0.1F;
            }
            return ret;
        }

        public Rectangle SearchForObjects(Image<Hsv, Byte> hsv)
        {
            rectangles = new List<Rectangle>();
            for (int i = 0; i < colors.Count / 2; i++)
            {
                Image<Gray, Byte> rangedImg = hsv.InRange(colors[i], colors[i + 1]);
                Rectangle rectangle = rec.getBoundingBox(rangedImg);
                if (rectangle.Height != 0)
                {
                    rectangles.Add(rectangle);
                }
            }
            if (rectangles.Count == 0)
                return new Rectangle(0, 0, 0, 0);
            else
                return biggestRectangle(rectangles);
        }

        

        

        public Image<Gray, Byte> EnchancedImage(double[] rgb1, double[] rgb2)
        {
            DetectObject();
            Image<Hsv, byte> hsv = currentImage.Convert<Hsv, byte>();
            Hsv p1 = new Hsv(rgb1[0], rgb1[1], rgb1[2]);
            Hsv p2 = new Hsv(rgb2[0], rgb2[1], rgb2[2]);
            Rectangle rec = SearchForObjects(hsv);

            Image<Gray, Byte> rangedImg = hsv.InRange(colors[2], colors[3]);
            Gray col = new Gray(100);
            rangedImg.Draw(rec, col, 2);
            return rangedImg;
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            if (cam != null)
                cam.Dispose();
        }
    }
}