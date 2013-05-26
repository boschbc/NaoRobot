using System;
using System.Net;
using System.Net.Sockets;

using Naovigate.Util;

namespace Naovigate.Communication
{
    /// <summary>
    /// A class representing the GOAL server.
    /// </summary>
    public class GoalServer
    {
        private static GoalServer instance;
        private static string[] seperators = { ",", " ", ":", ";", "-", "+" };

        TcpClient client;
        private CommunicationStream goalStream;
        private bool running = false;
        private TcpClient client;

        /// <summary>
        /// Establish a connection to the default server IP and port.
        /// </summary>
        public GoalServer()
        {
            Start(GoalCommunicator.DefaultIP, GoalCommunicator.DefaultPort);
        }

        /// <summary>
        /// Establish a connection to given server IP and port.
        /// </summary>
        /// <param name="ip">A string containing an IP.</param>
        /// <param name="port">Port number.</param>
        public void Start(string ip, int port)
        {
            if (running)
                return;
            
            TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();
            client = listener.AcceptTcpClient();
            goalStream = new CommunicationStream(client.GetStream());
            listener.Stop();
            running = true;
        }

        /// <summary>
        /// Closes the server.
        /// </summary>
        public void Close()
        {
            client.Close();
        }

        /// <summary>
        /// The GoalServer singleton's instance.
        /// </summary>
        public static GoalServer Instance
        {
            get { return instance == null ? instance = new GoalServer() : instance; }
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
                catch (FormatException)
                {
                    Logger.Log("Could not parse incoming data: " + arg);
                }
            }

        }

        /// <summary>
        /// Splits a string according to a set of internally defined delimeters.
        /// </summary>
        /// <param name="arg">The string to be split.</param>
        /// <returns>An array of strings.</returns>
        private string[] Split(string arg)
        {
            return arg.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Parses incoming data.
        /// </summary>
        /// <param name="args">One or more strings containing formatted data.</param>
        public static void Execute(params string[] args)
        {
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
