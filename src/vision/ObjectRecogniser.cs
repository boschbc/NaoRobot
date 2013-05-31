using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Aldebaran.Proxies;
using System.Collections;
using Naovigate.Util;
using Emgu.CV.VideoSurveillance;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Drawing;

namespace Naovigate.Vision
{
    public class ObjectRecogniser
    {
        private BlobDetector detector = new BlobDetector(0);

        public static ObjectRecogniser instance = null;

        public static ObjectRecogniser GetInstance()
        {
            return instance == null ? instance = new ObjectRecogniser() : instance;
        }
        
        //returns 0,0,0,0 when there is no object in the image returns the bounding box of the object otherwise
        public RectangleF getBoundingBox(Image<Gray, Byte> img)
        {
            BlobSeq seq = GetBlobs(img);
            if (seq.Count > 0)
            {
                MCvBlob blob = GetLargest(seq);
                if (GetSize(blob) >= 100)
                {
                    return (RectangleF)GetLargest(seq);
                }
            }
            return new RectangleF(0, 0, 0, 0);
        }

        private BlobSeq GetBlobs(Image<Gray, Byte> img)
        {
            img = img.Copy();
            img._Dilate(2);
            img._Erode(2);
            BlobSeq seq = new BlobSeq();
            detector.DetectNewBlob(img, seq, null);
            return seq;
        }

        private MCvBlob GetLargest(BlobSeq seq)
        {
            MCvBlob mb = seq.ElementAtOrDefault(0);
            float max = 0;
            foreach (MCvBlob blob in seq)
            {
                float size = GetSize(blob);
                if (size > max)
                {
                    max = size;
                    mb = blob;
                }
            }
            return mb;
        }

        private float GetSize(MCvBlob blob)
        {
            SizeF size = blob.Size;
            return size.Height * size.Width;
        }
    }
}
