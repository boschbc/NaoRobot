using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Aldebaran.Proxies;
using System.Collections;
using Naovigate.Util;
using Emgu.CV.Cvb;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.Util;
using System.Drawing;

namespace Naovigate.Vision
{
    public sealed class ObjectRecogniser
    {
        public static ObjectRecogniser instance = null;

        public static ObjectRecogniser Instance
        {
            get{
                return instance == null ? instance = new ObjectRecogniser() : instance;
            }
        }

        private static CvBlob GetLargest(CvBlobs blobs)
        {
            CvBlob mb = null;
            int max = 0;
            foreach (CvBlob blob in blobs.Values)
            {
                int size = blob.Area;
                if (size > max)
                {
                    max = size;
                    mb = blob;
                }
            }
            return mb;
        }

        private CvBlobDetector detector = new CvBlobDetector();

        //returns 0,0,0,0 when there is no object in the image returns the bounding box of the object otherwise
        public Rectangle getBoundingBox(Image<Gray, Byte> img)
        {
            CvBlobs blobs = GetBlobs(img);
            if (blobs.Count > 0)
            {
                CvBlob blob = GetLargest(blobs);
                if (blob.Area >= 100)
                {
                    return blob.BoundingBox;
                }
            }
            return new Rectangle(0, 0, 0, 0);
        }

        private CvBlobs GetBlobs(Image<Gray, Byte> img)
        {
            Image<Gray, Byte> im = img.Clone();
            im._Dilate(2);
            im._Erode(2);
            CvBlobs blobs = new CvBlobs();
            detector.Detect(im, blobs);
            return blobs;
        }
    }
}
