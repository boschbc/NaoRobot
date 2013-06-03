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
using Naovigate.Movement;
namespace Naovigate
{
    class MainProgram
    {
        public static readonly int NaoPort = 9559;
        public static readonly int GoalPort = 6747;
        public static readonly string LocalHost = "127.0.0.1";
        public static readonly string nao2 = "192.168.0.108";
        public static readonly string NaoIP = LocalHost;
        public static readonly string GoalIP = LocalHost;

        public static void Main(String[] args)
        {
            Setup();
            DialogResult useDebugGui = MessageBox.Show("Do you wish to use the NaoDebugger?", "Use Debugger?", MessageBoxButtons.YesNo);
            if (useDebugGui == DialogResult.Yes)
                LaunchDebugger.DebugMain();
            else
            {
                NaoState.Instance.Connect(nao2, 9559);
                MarkerSearchThread m = new MarkerSearchThread(64, 0);
                m.Start();
                //new TestingGoalServer().Start();
                //GoalCommunicator c = new GoalCommunicator("127.0.0.1", GoalCommunicator.DefaultPort);
                //c.StartAsync();

            }

            Console.WriteLine("Done");
            Console.Read();
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
