using System;
using System.Collections.Generic;
using System.Drawing;
using Naovigate.Util;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Naovigate.Vision
{
    internal sealed class Processing : IDisposable
    {

        private static int closeEnough = 150;
        /// <summary>
        /// Indicate if the given rectangle is an indication of being
        /// close enough to this object.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool CloseEnough(Rectangle rect)
        {
            return rect.Width >= closeEnough;
        }

        /// <summary>
        /// returns the biggest rectangle object
        /// </summary>
        /// <param name="rectangles"></param>
        /// <returns></returns>
        public static Rectangle BiggestRectangle(List<Rectangle> rectangles)
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

        private static Processing instance;

        public static Processing Instance
        {
            get { return instance == null ? new Processing(new Camera("Processing singleton")) : instance; }
            set { instance = value; }
        }

        private Camera cam;
        private Image<Rgb, Byte> currentImage;
        private List<Hsv> colors;
        ObjectRecogniser rec;
        List<Rectangle> rectangles;

        public Processing(Camera camera)
        {
            cam = camera;
            cam.Subscribe();
            rec = ObjectRecogniser.Instance;
            InitColors();
        }

        private void InitColors()
        {
            InitDefaultColors();
        }

        private void InitCalibColors()
        {
            // no default calibration, insert its values
            if (!Calibration.Instance.Path.Contains("efault"))
            {
                int close = Calibration.Instance.GetRecord<int>("CloseToObjectDistance");
                if (close != default(int))
                {
                    closeEnough = close;
                    Logger.Log(this, "CloseToObjectDistance = " + close);
                }
            }
            //TODO hsv values from calibration
        }

        private void InitDefaultColors()
        {
            colors = new List<Hsv>();
            Hsv redObjectMin = new Hsv(255, 255, 255);
            Hsv redObjectMax = new Hsv ( 255.0, 255.0, 255.0);
            Hsv greenObjectMin = new Hsv ( 80, 150, 43 );
            Hsv greenObjectMax = new Hsv ( 116, 199, 180 );
            Hsv blueObjectMin = new Hsv (75, 180, 0);
            Hsv blueObjectMax = new Hsv (125, 255, 125);
            colors.Add(redObjectMin);
            colors.Add(redObjectMax);
            colors.Add(blueObjectMin);
            colors.Add(blueObjectMax);
            colors.Add(greenObjectMin);
            colors.Add(greenObjectMax);
           
        }

        //checks of there is a object in sight and then returns a array with distance and angle
        public Rectangle DetectObject()
        {
            currentImage = cam.GetImage();
            // image null, interrupted during transmission
            if (currentImage == null) return new Rectangle(0, 0, 0, 0);
            Image<Hsv, Byte> hsvImg = currentImage.Convert<Hsv, Byte>();
            return SearchForObjects(hsvImg);
        }

        public Boolean ObjectInSight()
        {
            return DetectObject().Width > 0;
        }

        public float CalculateTheta(Rectangle rect)
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
                return BiggestRectangle(rectangles);
        }        

        public Image<Gray, Byte> EnchancedImage(double[] rgb1, double[] rgb2)
        {
            DetectObject();
            Image<Hsv, byte> hsv = currentImage.Convert<Hsv, byte>();
            Hsv p1 = new Hsv(rgb1[0], rgb1[1], rgb1[2]);
            Hsv p2 = new Hsv(rgb2[0], rgb2[1], rgb2[2]);
            Rectangle rec = SearchForObjects(hsv);

            Image<Gray, Byte> rangedImg = hsv.InRange(colors[2], colors[3]);
            //Image<Gray, Byte> rangedImg = hsv.InRange(p1, p2);
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