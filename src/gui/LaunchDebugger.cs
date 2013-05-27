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
            NaoState.Instance.Connect(MainProgram.ip, MainProgram.port);
            Application.Run(new NaoDebugger());
            ExitDebugger();
        }

        private static void ExitDebugger()
        {
            EventQueue.Nao.Post(new Event.Internal.CrouchEvent());
            EventQueue.Nao.WaitFor();
            EventQueue.Nao.Abort();
            NaoState.Instance.Disconnect();
            GoalCommunicator.Instance.Close();
            GoalServer.Instance.Close();
        }
    }
}
