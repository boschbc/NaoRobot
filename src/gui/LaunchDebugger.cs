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
        public static void DebugMain(params String[] args)
        {
            string ip= "127.0.0.1";
            if (args.Length > 0)
                ip = args[0];
            
            int port = 9559;
            NaoState.Instance.Connect(ip, port);
            new Thread(new ThreadStart(StartGoalCommunicator)).Start();
            Application.Run(new NaoDebugger());
        }

        public static void StartGoalCommunicator()
        {
            GoalCommunicator.Instance.Connect();
        }
    }
}
