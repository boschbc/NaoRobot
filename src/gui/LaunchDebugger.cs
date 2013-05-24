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
        private static readonly string goalserverIP = "127.0.0.1";
        private static readonly int goalserverPort = 6474;
        private static readonly string naoIP = "127.0.0.1"; //"192.168.0.126";
        private static readonly int naoPort = 9559;

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
            GoalCommunicator.Instance.Start();
        }

        private static void StartDebugger()
        {
            NaoState.Instance.Connect(naoIP, naoPort);
            Application.Run(new NaoDebugger());
        }
    }
}
