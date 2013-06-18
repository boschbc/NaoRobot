using System;
using System.Threading;
using System.Windows.Forms;
using Naovigate.Communication;
using Naovigate.GUI;
using Naovigate.Movement;
using Naovigate.Util;
using System.Collections;
namespace Naovigate
{
    public static class MainProgram
    {
        public static readonly int NaoPort = 9559;
        public static readonly int GoalPort = 6747;
        public static readonly string LocalHost = "127.0.0.1";
        public static readonly string nao2 = "192.168.0.107";
        public static readonly string soccer = "192.168.0.101";
        public static readonly string goalIP1 = "192.168.0.108";
        public static readonly string NaoIP = nao2;
        public static readonly string GoalIP = goalIP1;

        public static void Main(String[] args)
        {
            try
            {
                Setup();
                DialogResult useDebugGui = MessageBox.Show("Do you wish to use the NaoDebugger?", "Use Debugger?", MessageBoxButtons.YesNo);
                if (useDebugGui == DialogResult.Yes)
                    LaunchDebugger.DebugMain();
                else
                    Test();
                Console.Read();
            }
            catch(Exception e)
            {
                Logger.Say(e.GetType().Name+" "+e.Message);
            }
        }

        public static void Test()
        {
            Calibration.Instance = new Calibration("../resources/calibs/mario.naocalib");
            Logger.Log(Calibration.Instance.GetRecord<int>("SRedMin"));
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
