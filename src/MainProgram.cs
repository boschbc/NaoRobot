using System;
using System.Net;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Testing.GUI;
using Naovigate.Vision;
using System.Collections;
using Aldebaran.Proxies;
using System.Collections.Generic;

namespace Naovigate
{
    class MainProgram
    {
        public static void Main(String[] args)
        {
            ShutDownHook();
            //Use this switch to deactivate debugger invocation:
            bool Debug = false;
            string localhost = "127.0.0.1";
            string Nao = "192.168.0.128";
            if (Debug)
            {
                LaunchDebugger.DebugMain(localhost);
                return;
            }
            //NaoState.Instance.Connect(Nao, 9559);
            //Camera cm = new Camera("awesome");
            //cm.StartVideo();
            //cm.CallibrateCamera(3);
            //System.Threading.Thread.Sleep(1000);
            //cm.StopVideo();

            ObjectRecogniser recognizer = new ObjectRecogniser(Nao, 9559);
            //recognizer.InsertVisionDataBase();
            Console.WriteLine("Connected");
            bool found = false;
            while (found == false)
            {
                Console.WriteLine("Starting");
                ArrayList objectdata = (ArrayList)recognizer.GetObjectData();
                Console.WriteLine(objectdata.Count);
                if (objectdata.Count != 0)
                {
                    Console.WriteLine("got it");
                    //ArrayList pictureInfo = (ArrayList)objectdata[1];
                    //Console.WriteLine("pictureinfo count:" + pictureInfo.Count);
                    //ArrayList labels = (ArrayList)pictureInfo[0];
                    //Console.WriteLine("labels count: " + labels.Count);
                    //ArrayList labels0 = (ArrayList)labels[0];
                    //Console.WriteLine("labels0 count: " + labels0.Count);
                    //Console.WriteLine("labels0 0: " + labels0[0]);
                    //Console.WriteLine("labels0 0: " + labels0[1]);
                }
                else
                {
                    Console.WriteLine("fail");
                }
                System.Threading.Thread.Sleep(3000);
            }
        }

        private static void ShutDownHook()
        {
             AppDomain.CurrentDomain.ProcessExit += Cleanup;  
        }

        private static void Cleanup(object sender, EventArgs e)
        {
            Console.WriteLine("Shutting down...");
            NaoState.Instance.Disconnect();
        }
    }
}
