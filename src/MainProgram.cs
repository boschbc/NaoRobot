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
        public static readonly int port = 9559;
        public static readonly string localhost = "127.0.0.1";
        public static readonly string nao = "192.168.0.128";

        // Use this switch to deactivate debugger invocation:
        public static readonly bool useDebugGui = false;

        public static void Main(String[] args)
        {
            ShutDownHook();
            if (useDebugGui)
                LaunchDebugger.DebugMain(localhost);
            else
                Stuff();
        }

        public static void Stuff()
        {
            NaoState.Instance.Connect(nao, 9559);
            //Grabbing.CoolGrabber grabber = Grabbing.CoolGrabber.Instance;
            //grabber.doSomething();

            MotionProxy motion = NaoState.Instance.MotionProxy;
            motion.setWalkArmsEnable(false, false);
            motion.moveToward(0.2F, 0, 0);
        }

        private static void TmpTest()
        {
            Console.WriteLine("Connect");
            NaoState.Instance.Connect(nao, port);

            NaoState.Instance.MotionProxy.wakeUp();
            Console.WriteLine("Put Down");
            Naovigate.Movement.Pose.Instance.Welcome();
            Naovigate.Grabbing.Grabber.Instance.PutDown();
            
            Console.WriteLine("End");
            System.Threading.Thread.Sleep(1000);
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
