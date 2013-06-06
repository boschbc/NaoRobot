using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

using Aldebaran.Proxies;

using Naovigate.Movement;
using Naovigate.Communication;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;
using Naovigate.Testing.GUI;
using Naovigate.Vision;
using Naovigate.Event;
namespace Naovigate
{
    class MainProgram
    {
        public static readonly int NaoPort = 9559;
        public static readonly int GoalPort = 6747;
        public static readonly string LocalHost = "127.0.0.1";
        public static readonly string nao2 = "192.168.0.128";
        public static readonly string NaoIP = nao2;
        public static readonly string GoalIP = LocalHost;

        public static void Main(String[] args)
        {
            Setup();
            DialogResult useDebugGui = MessageBox.Show("Do you wish to use the NaoDebugger?", "Use Debugger?", MessageBoxButtons.YesNo);
            if (useDebugGui == DialogResult.Yes)
                LaunchDebugger.DebugMain();
            else
            {
                NaoState.Instance.Connect(NaoIP, 9559);
                Pose.Instance.StandUp();
                Pose.Instance.Look(0.5F);
                Camera cam = new Camera("Processing");
                cam.Subscribe();
                cam.CalibrateCamera(3);
                Processing p = new Processing(cam);
                
                    NaoState.Instance.Connect(NaoIP, 9559);
                    ObjectSearchThread search = new ObjectSearchThread(1);
                    search.Run();
            }

            Console.WriteLine("Done");
            Console.Read();
        }

        public static void Test()
        {

        }

        private static void Setup()
        {
            Logger.Clear();
            AppDomain.CurrentDomain.ProcessExit += Cleanup;
        }

        private static void Cleanup(object sender, EventArgs e)
        {
            Logger.Log(typeof(MainProgram), "Performing clean-up...");
            GoalCommunicator.Instance.Close();
            NaoState.Instance.Disconnect();
            Logger.Log(typeof(MainProgram), "Program terminated.");
        }
    }
}
