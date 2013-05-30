using System;
using System.Net;
using System.IO;
using System.Net.Sockets;
using Naovigate.Util;
using System.Threading;

namespace Naovigate.Communication
{
    class TestingGoalServer
    {
        private TcpClient c;
        public void Start()
        {
            new Thread(new ThreadStart(Run)).Start();
        }

        public void Run()
        {
            Start("127.0.0.1", GoalCommunicator.DefaultPort);
        }

        public void Start(string ip, int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
            while (true)
            {
                Logger.Log(this, "Starting GOAL server...");
                listener.Start();
                TcpClient client = listener.AcceptTcpClient();
                while (c != null) ;
                c = client;
                new Thread(new ThreadStart(Handler)).Start();
            }
        }

        public void Handler()
        {
            TcpClient client = c;
            c = null;
            Stream stream = client.GetStream();
            bool running = true;
            while (running)
            {
                Thread.Sleep(100);
                stream.WriteByte(0xff);
                Logger.Log(this, "Got: "+stream.ReadByte());
                if (new System.Random().Next(0, 10) == 0)
                {
                    running = false;
                    stream.Close();
                }
            }
        }
    }
}
