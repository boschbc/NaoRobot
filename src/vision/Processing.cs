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
        private List<Hsv> colors;
        ObjectRecogniser rec;

        public Processing(Camera cm)
        {
            cam = cm;
            rec = ObjectRecogniser.Instance();
            Init();
        }

        public void Init()
        {
            Hsv redObjectMIn = new Hsv(0.0, 0.0, 0.0);
            Hsv redObjectMax = new Hsv ( 180.0, 255.0, 255.0);
            Hsv greenObjectMin = new Hsv ( 63, 66, 43 );
            Hsv greenObjectMax = new Hsv ( 116, 199, 123 );
            Hsv blueObjectMin = new Hsv (0, 0, 0);
            Hsv blueObjectMax = new Hsv (255, 255, 255);
            colors.Add(redObjectMIn);
            colors.Add(redObjectMax);
            colors.Add(greenObjectMin);
            colors.Add(greenObjectMax);
            colors.Add(blueObjectMin);
            colors.Add(blueObjectMax);
        }

        public ArrayList DetectObject()
        {
            currentImage = cam.GetImage();
            Image<Hsv, Byte> hsvImg = currentImage.Convert<Hsv, Byte>();
            Rectangle objectRectangle = SearchForObjects(hsvImg);
            if (objectRectangle.Height == 0)
                return null;
            else
            {

                ArrayList objdata = ObjectData(objectRectangle);
                return objdata;
            }  
        }

        public Rectangle SearchForObjects(Image<Hsv, Byte> hsv)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
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

        public ArrayList ObjectData(Rectangle objectRectangle)
        {   
            return null;
        }

        public Rectangle biggestRectangle(List<Rectangle> rectangles)
        {
            Console.WriteLine("to be implemented");
            return rectangles[0];
        }

        public double ObjectAngle(Image<Hsv,byte> img)
        {
            return 0.0;
        }
        public double ObjectDistance(Image<Hsv, byte> img)
        {
            return 0.0;
        }


        public Image<Gray, Byte> EnchancedImage(double[] rgb1, double[] rgb2)
        {
            currentImage = cam.GetImage();
            
            Hsv p1 = new Hsv(63.0,66.0,43.0);
            Hsv p2 = new Hsv(116, 199, 123);
            Image<Hsv, byte> hsv = currentImage.Convert<Hsv, byte>();

            Rectangle rec = SearchForObjects(hsv);

            Image<Gray, Byte> gray = hsv.InRange(p1, p2);
            Gray gr = new Gray(100.0);
            gray.Draw(rec, gr, 5);
            return gray;
        }
    }
}

