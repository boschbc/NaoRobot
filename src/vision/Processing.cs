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

        private static int closeEnough = 170;
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
            get { return instance == null ? instance = new Processing(new Camera("Processing singleton")) : instance; }
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
            if (Calibration.Initialized)
                InitCalibColors();
            else
                InitDefaultColors();
        }

        private void InitCalibColors()
        {
            try
            {
                Calibration c = Calibration.Instance;
                int close = c.GetRecord<int>("CloseToObjectDistance");
                if (close != default(int))
                {
                    closeEnough = close;
                    Logger.Log(this, "CloseToObjectDistance = " + close);
                }
                colors = new List<Hsv>();
                
                Hsv redObjectMin = new Hsv(c.GetRecord<int>("HRedMin"),
                                           c.GetRecord<int>("SRedMin"),
                                           c.GetRecord<int>("VRedMin"));
                Hsv redObjectMax = new Hsv(c.GetRecord<int>("HRedMax"),
                                           c.GetRecord<int>("SRedMax"),
                                           c.GetRecord<int>("VRedMax"));
                Hsv greenObjectMin = new Hsv(c.GetRecord<int>("HGreenMin"),
                                             c.GetRecord<int>("SGreenMin"),
                                             c.GetRecord<int>("VGreenMin"));
                Hsv greenObjectMax = new Hsv(c.GetRecord<int>("HGreenMax"),
                                             c.GetRecord<int>("SGreenMax"),
                                             c.GetRecord<int>("VGreenMax"));
                Hsv blueObjectMin = new Hsv(c.GetRecord<int>("HBlueMin"),
                                            c.GetRecord<int>("SBlueMin"),
                                            c.GetRecord<int>("VBlueMin"));
                Hsv blueObjectMax = new Hsv(c.GetRecord<int>("HBlueMax"),
                                            c.GetRecord<int>("SBlueMax"),
                                            c.GetRecord<int>("VBlueMax"));
                colors.Add(redObjectMin);
                colors.Add(redObjectMax);
                colors.Add(blueObjectMin);
                colors.Add(blueObjectMax);
                colors.Add(greenObjectMin);
                colors.Add(greenObjectMax);
            }
            catch
            {
                InitDefaultColors();
                Logger.Log(this, "CATCHING!");
            }
        }

        private void InitDefaultColors()
        {
            colors = new List<Hsv>();
            Hsv redObjectMin = new Hsv(0, 180, 10);
            Hsv redObjectMax = new Hsv (100, 255, 255);
            Hsv blueObjectMin = new Hsv (100, 150, 0);
            Hsv blueObjectMax = new Hsv (140, 255, 200);
            Hsv greenObjectMin = new Hsv(40, 120, 40);
            Hsv greenObjectMax = new Hsv(100, 255, 200);
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
            for (int i = 0; i < colors.Count; i= i+2)
            {
                Image<Gray, Byte> rangedImg = hsv.InRange(colors[i], colors[i + 1]);
                Rectangle rectangle = rec.getBoundingBox(rangedImg);
                if (rectangle.Height != 0)
                {
                    
                    rectangles.Add(rectangle);
                }
            }
            Logger.Log(this, rectangles.Count);
            if (rectangles.Count == 0)
                return new Rectangle(0, 0, 0, 0);
            else
                return BiggestRectangle(rectangles);
        }        

        public Image<Gray, Byte> EnchancedImage(double[] rgb1, double[] rgb2)
        {
            currentImage = cam.GetImage();
            if (currentImage != null)
            {
            
                Image<Hsv, byte> hsv = currentImage.Convert<Hsv, byte>();
                Hsv p1 = new Hsv(rgb1[0], rgb1[1], rgb1[2]);
                Hsv p2 = new Hsv(rgb2[0], rgb2[1], rgb2[2]);
                Rectangle rec = SearchForObjects(hsv);

                //Image<Gray, Byte> rangedImg = hsv.InRange(colors[2], colors[3]);
                Image<Gray, Byte> rangedImg = hsv.InRange(p1, p2);
                Gray col = new Gray(100);
                rangedImg.Draw(rec, col, 2);
                return rangedImg;
            }
            else
            {
               return new Image<Gray, byte>(160, 120);
            }
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