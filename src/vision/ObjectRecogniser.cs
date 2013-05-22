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

        public static VisionRecognitionProxy instance = null;

        public static VisionRecognitionProxy GetInstance()
        {
            return instance == null ? instance = new VisionRecognitionProxy(NaoState.Instance.IP.ToString(), NaoState.Instance.Port) : instance;
        }

        public ObjectRecogniser(String ip, int port)
        {
            objectRecognizer = new VisionRecognitionProxy(ip, port);
            objectRecognizer.subscribe("VisionRecognizer", 1000, 0F);
            memory = new MemoryProxy(ip, port);
            
        }

        //returns object data
        public ArrayList GetObjectData()
        {
            try
            {
                return (ArrayList)memory.getData("PictureDetected");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ArrayList res = new ArrayList();
                return res;

            }
        }

        public void InsertVisionDataBase()
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
