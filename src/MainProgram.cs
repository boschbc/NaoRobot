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
using Naovigate.GUI;
using Naovigate.Vision;
using Naovigate.Event;
namespace Naovigate
{
    public static class MainProgram
    {
        public static readonly int NaoPort = 9559;
        public static readonly int GoalPort = 6747;
        public static readonly string LocalHost = "127.0.0.1";
        public static readonly string nao2 = "192.168.0.108";
        public static readonly string tutor = "192.168.0.126";
        public static readonly string goalIP1 = "192.168.0.116";
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
               
            }
        }

        public static void Test()
        {
            Pose.Instance.Look(0.5f);
        }

        private static void Setup()
        {
            Thread cur = Thread.CurrentThread;
            if (cur.Name == null) cur.Name = "MainProgram";
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
