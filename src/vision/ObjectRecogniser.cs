using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Aldebaran.Proxies;
using System.Collections;
using Naovigate.Util;

namespace Naovigate.Vision
{
    class ObjectRecogniser
    {
        private VisionRecognitionProxy objectRecognizer;
        private MemoryProxy memory;
        private Processing imageProcessor;

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
            imageProcessor = new Processing("vision");
            memory = NaoState.Instance.MemoryProxy;
        }

        public static float estimateDistance(float sizeX)
        {
            return (1 / sizeX) / FRANKENAO2C;
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
