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
    class ObjectRecogniser
    {
        private VisionRecognitionProxy objectRecognizer;
        private MemoryProxy memory;
        private Processing imageProcessor;

        BlobDetector detector = new BlobDetector(0);

        public static ObjectRecogniser instance = null;

        public static float FRANKENAO2C = 5.414f;

        public static ObjectRecogniser GetInstance()
        {
            return instance == null ? instance = new ObjectRecogniser() : instance;
        }

        public ObjectRecogniser()
        {
            objectRecognizer = NaoState.Instance.ObjectDetectionProxy;
            objectRecognizer.subscribe("VisionRecognizer", 1000, 0F);
            Camera cm = new Camera("VisionRecognizer");
            imageProcessor = new Processing(cm);
            memory = NaoState.Instance.MemoryProxy;
        }

        public static float estimateDistance(float sizeX)
        {
            return (1 / sizeX) / FRANKENAO2C;
        }

        //returns 0,0,0,0 when there is no object in the image returns the bounding box of the object otherwise
        public RectangleF getRectangle(Image<Gray, Byte> img)
        {
            BlobSeq seq = GetBlobs(img);
            if (seq.Count == 0) return new RectangleF(0,0,0,0);
            else return (RectangleF)GetLargest(seq);
        }

        public BlobSeq GetBlobs(Image<Gray, Byte> img) {
            img = img.Copy();
            img._Dilate(2);
            img._Erode(2);
            BlobSeq seq = new BlobSeq();
            detector.DetectNewBlob(img, seq, null);
            return seq;
        }

        public MCvBlob GetLargest(BlobSeq seq)
        {
            MCvBlob mb = seq.ElementAtOrDefault(0);
            float max = 0;
            foreach (MCvBlob blob in seq) {
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

        //returns object data
        public ArrayList GetObjectData()
        {
            return imageProcessor.DetectObject();
        }

        public void InsertVisionDatabase()
        {
            try
            {
                Console.WriteLine("input database");
                Console.WriteLine(objectRecognizer.changeDatabase("C:/Users/Bert/Documents/GitHub/NaoRobot/src/CocaColaNao.vrd", "CocaColaNao3"));
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("ObjectRecognizer databasepush error: " + e);
            }
        }
    }
}
