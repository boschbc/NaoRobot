using System;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Communication;
using Naovigate.GUI;
using Naovigate.Util;

namespace Naovigate.Testing.GUI
{
    class LaunchDebugger
    {
        private static string goalserverIP = "127.0.0.1";
        private static int goalserverPort = 6474;
        private static string naoIP = "192.168.0.126";
        private static int naoPort = 9559;

        public static void DebugMain()
        {
            new Thread (new ThreadStart(StartGoalServer)).Start();
            new Thread (new ThreadStart(StartGoalCommunication)).Start();
            StartDebugger();
        }

        private static void StartGoalServer()
        {
            GoalStub.Instance.StartServer(goalserverIP, goalserverPort);
        }

        private static void StartGoalCommunication()
        {
            //GoalCommunicator.Instance.Connect();
            GoalCommunicator.Instance.Start();
        }

        private static void StartDebugger()
        {
            NaoState.Instance.Connect(naoIP, naoPort);
            Application.Run(new NaoDebugger());
        }
    }
}
