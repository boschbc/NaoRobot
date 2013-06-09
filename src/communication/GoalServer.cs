using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Naovigate.Util;

namespace Naovigate.Communication
{
    /// <summary>
    /// A class representing the GOAL server.
    /// </summary>
    public class GoalServer
    {
        private static GoalServer instance;
        private static string[] seperators = { ",", " " };

        private ICommunicationStream goalStream;
        private TcpListener listener;
        private TcpClient client;

        /// <summary>
        /// The GoalServer singleton's instance.
        /// </summary>
        public static GoalServer Instance
        {
            get { return instance == null ? instance = new GoalServer() : instance; }
        }

        /// <summary>
        /// Create a new server instance.
        /// </summary>
        public GoalServer() { }

        /// <summary>
        /// True if the server is currently running.
        /// </summary>
        public Boolean Running
        {
            get;
            private set;
        }

        /// <summary>
        /// Start a server listening to given IP and Port.
        /// </summary>
        /// <param name="ip">A string containing an IP.</param>
        /// <param name="port">Port number.</param>
        public void Start(string ip, int port)
        {
            if (Running)
                return;
            Logger.Log(this, "Starting GOAL server...");
            new Thread(() => Listen(ip, port)).Start();
            Running = true;
            Logger.Log(this, "Server is running.");
        }

        public void Listen(string ip, int port)
        {
            Logger.Log(this, "Listening...");
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();
            try
            {
                client = listener.AcceptTcpClient();
                Logger.Log(this, "Accepted a client.");
                goalStream = new BitStringCommunicationStream(client.GetStream());
                listener.Stop();
                Logger.Log(this, "Stopped listening.");
            }
            catch (SocketException)
            {
                Logger.Log(this, "Interrupted while listening.");
            }
        }
        
        /// <summary>
        /// Sends data to the client.
        /// </summary>
        /// <param name="arg">A string representing the incoming data-stream.</param>
        private void SendData(string arg)
        {
            if (!Running)
                return;

            String[] ss = Split(arg);
            
            for (int i = 0; i < ss.Length;i++ )
            {
                try
                {
                    if (ss[i].Length > 1 && (ss[i].Contains("b") || ss[i].StartsWith("0x")))
                        WriteAsByte(ss[i]);
                    else if (ss[i].Length > 0)
                        goalStream.WriteInt(Convert.ToInt32(ss[i]));
                }
                catch (FormatException)
                {
                    Logger.Log(this, "Could not parse incoming data: " + arg);
                }
            }

        }

        private void WriteAsByte(string arg)
        {
            if (arg.StartsWith("0x"))
                goalStream.WriteByte(Convert.ToByte(arg.Replace("0x", ""), 16));
            else if(arg.Contains("b"))
                goalStream.WriteByte(Convert.ToByte(arg.Replace(@"b", "")));
            
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
        /// Sends one or more instances of data to the client.
        /// </summary>
        /// <param name="args">One or more strings containing formatted data.</param>
        public void SendDataRange(params string[] args)
        {
            if (!Running)
                return;
            for (int i = 0; i < args.Length; i++)
            {
                SendData(args[i]);
            }
        }

        /// <summary>
        /// Closes the server.
        /// </summary>
        public void Close()
        {
            Logger.Log(this, "Closing GOAL server...");
            try
            {
                if (listener != null)
                    listener.Stop();
                if (client != null)
                    client.Close();
                Running = false;
                Logger.Log(this, "Closed.");
            }
            catch (SocketException e)
            {
                Logger.Log(this, "Exception caught while closing GOAL server:\n" + e);
            }
        }
    }
}
