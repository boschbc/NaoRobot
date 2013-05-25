using System;
using System.Net;
using System.Net.Sockets;

using Naovigate.Communication;

namespace Naovigate.GUI
{
    public class GoalStub
    {
        private CommunicationStream goalStream;
        private static GoalStub instance;
        private bool running = false;
        private TcpClient client;

        public GoalStub()
        {
            StartServer(GoalCommunicator.defaultIp, GoalCommunicator.defaultPort);
        }

        public void StartServer(string ip, int port)
        {
            if (running)
                return;
            
            TcpListener l = new TcpListener(IPAddress.Parse(ip), port);
            l.Start();
            client = l.AcceptTcpClient();
            goalStream = new CommunicationStream(client.GetStream());
            l.Stop();
            running = true;
        }

        public static GoalStub Instance
        {
            get { return instance == null ? instance = new GoalStub() : instance; }
        }

        /// <summary>
        /// Simulate a goal server sending arguments to the GoalCommunicator.
        /// </summary>
        /// <param name="arg">A string representing the incoming data-stream.</param>
        private void ExecuteArguments(string arg)
        {
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

        public static void Abort()
        {
            try
            {
                if (instance != null && instance.client != null)
                {
                    instance.client.Close();
                    Console.WriteLine("GoalStub Closed");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("GoalStub: "+e);
            }
        }
    }
}
