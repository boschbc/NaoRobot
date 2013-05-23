using System;
using System.Text;
using Naovigate.Communication;
using System.Net;
using System.Net.Sockets;

namespace Naovigate.GUI
{
    public class GoalStub
    {
        private CommunicationStream goalStream;
        private static GoalStub instance;
        private bool serverRunning = false;

        public GoalStub()
        {
            StartServer(GoalCommunicator.defaultIp, GoalCommunicator.defaultPort);
        }

        public void StartServer(string ip, int port)
        {
            if (serverRunning)
                return;
            Console.WriteLine("GoalStub.StartServer");
            TcpListener l = new TcpListener(IPAddress.Parse(ip), port);
            l.Start();
            TcpClient client = l.AcceptTcpClient();
            goalStream = new CommunicationStream(client.GetStream());
            l.Stop();
            serverRunning = true;
        }

        public static GoalStub Instance
        {
            get { return instance == null ? instance = new GoalStub() : instance; }
        }

        /*
         * simulate the goal server sending the arguments to the GoalCommunicator
         */
        private void ExecuteArguments(string arg)
        {
            Console.WriteLine("GoalStub.ExecuteArguments");
            String[] ss = Split(arg);
            
            for (int i = 0; i < ss.Length;i++ )
            {
                Console.WriteLine(ss[i]);
                try
                {
                    if (ss[i].Length > 1 && ss[i].Contains("b"))
                    {
                        string tmp = ss[i].Replace(@"b", "");
                        goalStream.WriteByte(Convert.ToByte(tmp));
                    }
                    else if (ss[i].Length > 1 && ss[i].StartsWith("0x"))
                    {
                        string tmp = ss[i].Replace("0x", "");
                        goalStream.WriteByte(Convert.ToByte(tmp, 16));
                    }
                    else if (ss[i].Length > 0)
                    {
                        goalStream.WriteInt(Convert.ToInt32(ss[i]));
                    }
                }
                catch(FormatException)
                {
                    Console.WriteLine("Invalid Argument Parsing" + arg + ", found NaN.");
                }
            }

        }

        private string[] Split(string arg)
        {
            string[] seperators = { ",", " ", ":", ";", "-", "+" };
            return arg.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
        }

        /*
         * simulate the goal server sending the arguments to the GoalCommunicator
         */
        public static void Execute(params string[] args)
        {
            Console.WriteLine("GoalStub.Execute");
            for (int i = 0; i < args.Length; i++)
            {
                Instance.ExecuteArguments(args[i]);
            }
        }
    }
}
