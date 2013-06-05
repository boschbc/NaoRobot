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
        List<Rectangle> rectangles;
        double naoFactor = 2;

        public Processing(Camera camera)
        {
            cam = camera;
            rec = ObjectRecogniser.Instance;
            Init();
        }

        public void Init()
        {
            colors = new List<Hsv>();
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
        //checks of there is a object in sight and then returns a array with distance and angle
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

        //constructs a arraylist of data from the objectRectangle
        public ArrayList ObjectData(Rectangle objectRectangle)
        {
            ArrayList ret = new ArrayList();
            ret[0] = ObjectDistance(objectRectangle);
            ret[1] = ObjectAngle(objectRectangle);
            return ret;
        }

        //returns the biggest rectangle object
        public Rectangle biggestRectangle(List<Rectangle> rectangles)
        {
            Console.WriteLine("to be implemented");
            if(rectangles.Count ==2)
                return rectangles[1];            
            else
                return rectangles[0];
        }

        //determins the angle between the nao and the object
        public double ObjectAngle(Rectangle rec)
        {
            Point loc = rec.Location;
            double locX = loc.X;
            double centerX = currentImage.Width / 2;
            double deltaX = centerX - locX;
 
            double angle = Math.Tan(deltaX / ObjectDistance(rec));
            return angle;
        }

        //determins the distance between the object and the nao
        public double ObjectDistance(Rectangle rec)
        {
            double width = rec.Width;
            double height = rec.Height;
            double opp = width * height;

            double dist = opp / naoFactor;
            return dist;
        }


        public Image<Hsv, Byte> EnchancedImage(double[] rgb1, double[] rgb2)
        {
            DetectObject();
            Image<Hsv, byte> hsv = currentImage.Convert<Hsv, byte>();
            Hsv p1 = new Hsv(rgb1[0], rgb1[1], rgb1[2]);
            Hsv p2 = new Hsv(rgb2[0], rgb2[1], rgb2[2]);
            Rectangle rec = SearchForObjects(hsv);

            Hsv col = new Hsv(116,199,122);
            hsv.Draw(rec, col, 2);
            return hsv;
        }
    }
}

