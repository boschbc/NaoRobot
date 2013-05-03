using System;
using System.Net;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Testing.GUI;

namespace Naovigate
{
    class MainProgram
    {
        public static void Main(String[] args)
        {
            ShutDownHook();
            //Use this switch to deactivate debugger invocation:
            bool Debug = true;
            string localhost = "127.0.0.1";
            string tony = "192.168.0.110";
            string orangeNao = "192.168.0.109";
            if (Debug)
                LaunchDebugger.DebugMain(tony);
            //NaoProxyManager.Instance.EndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9550);
            //Testing.GoalCommunicatorTest1 goalCom = new Testing.GoalCommunicatorTest1(args);
            //hoi hoi
            //Sonar.Sonar sn = new Sonar.Sonar("192.168.0.126");
            //sn.deactivateSonar();
            //sn.activateSonar();
            //while (true)
            //{
            //    sn.getSonarDataLeft();
            //    sn.getSonarDataRight();
            //    Console.WriteLine();
            //    System.Threading.Thread.Sleep(100);
            //}

        }

        private static void ShutDownHook()
        {
             AppDomain.CurrentDomain.ProcessExit += Cleanup;  
        }

        private static void Cleanup(object sender, EventArgs e)
        {
            Console.WriteLine("Shutting down...");
            NaoState.Disconnect();
        }
    }
}
