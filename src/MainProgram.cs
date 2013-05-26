using System;
using System.Net;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Testing.GUI;
using Naovigate.Vision;
using System.Collections;
using Aldebaran.Proxies;
using System.Collections.Generic;
using Naovigate.Movement;

namespace Naovigate
{
    class MainProgram
    {
        public static readonly int port = 9559;
        public static readonly string localhost = "127.0.0.1";
        public static readonly string nao = "192.168.0.126";
        public static readonly string nao2 = "192.168.0.128";

        // Use this switch to deactivate debugger invocation:
        public static readonly bool useDebugGui = true;

        public static void Main(String[] args)
        {
            ShutDownHook();
            if (useDebugGui)
                LaunchDebugger.DebugMain();
            else
            {
                NaoState.Instance.Connect(localhost, port);
                Grabbing.Grabber.Instance.Grab();
                //Stuff();
                //Test();
            }

            Console.WriteLine("Done. Press any key to exit.");
            //Console.Read();
        }

        public static void Stuff()
        {
            //NaoState.Instance.Connect(nao, 9559);
            //Grabbing.CoolGrabber grabber = Grabbing.CoolGrabber.Instance;
            //grabber.doSomething();

            MotionProxy motion = NaoState.Instance.MotionProxy;
            motion.setWalkArmsEnable(false, false);
            motion.moveToward(0.2F, 0, 0);
        }

        private static void Test()
        {
            Console.WriteLine("Connect");
            //NaoState.Instance.Connect(localhost, port);

            NaoState.Instance.MotionProxy.wakeUp();
            Console.WriteLine("Do Test");

        }

        private static void ShutDownHook()
        {
            AppDomain.CurrentDomain.ProcessExit += Cleanup;
        }

        private static void Cleanup(object sender, EventArgs e)
        {
            Logger.Log(typeof(MainProgram), "Performing clean-up...");
            NaoState.Instance.Disconnect();
            Logger.Log(typeof(MainProgram), "Program terminated.");
        }
    }
}
