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
        public static readonly int port = 9559;
        public static readonly string localhost = "127.0.0.1";
        public static readonly string nao2 = "192.168.0.126";
        public static readonly string ip = localhost;

        public static void Main(String[] args)
        {
            Setup();
            DialogResult useDebugGui = MessageBox.Show("Do you wish to use the NaoDebugger?", "Use Debugger?", MessageBoxButtons.YesNo);
            if (useDebugGui.Equals(DialogResult.Yes))
                LaunchDebugger.DebugMain();
            else
            {
                NaoState.Instance.Connect(localhost, 9559);
                //new TestingGoalServer().Start();
                GoalCommunicator c = new GoalCommunicator("192.168.0.127", GoalCommunicator.DefaultPort);
                c.StartAsync();
                while (c.Coms == null) ;
                EventQueue.Goal.Post(new AgentEvent());
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
