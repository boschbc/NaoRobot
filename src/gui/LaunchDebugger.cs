using System;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.GUI;
using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.Testing.GUI
{
    class LaunchDebugger
    {
        //private static readonly string goalserverIP = "127.0.0.1";
        //private static readonly int goalserverPort = 6474;
        //private static readonly int naoPort = 9559;

        public static void DebugMain()
        {
            new Thread (new ThreadStart(StartGoalServer)).Start();
            new Thread (new ThreadStart(StartGoalCommunication)).Start();
            StartDebugger();
        }

        private static void StartGoalServer()
        {
            GoalServer i = GoalServer.Instance;//.Start(goalserverIP, goalserverPort);
        }

        private static void StartGoalCommunication()
        {
            GoalCommunicator.Instance.Start();
        }

        private static void StartDebugger()
        {
            NaoState.Instance.Connect(MainProgram.localhost, MainProgram.port);
            Application.Run(new NaoDebugger());
            ExitDebugger();
        }

        private static void ExitDebugger()
        {
            EventQueue.Nao.Post(new Event.Internal.SitDownEvent());
            EventQueue.Nao.Block();
            EventQueue.Nao.Terminate();
            NaoState.Instance.Disconnect();
            GoalCommunicator.Instance.Close();
            GoalServer.Instance.Close();
        }
    }
}
