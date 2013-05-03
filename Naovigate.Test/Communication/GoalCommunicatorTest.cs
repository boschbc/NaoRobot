using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Naovigate.Communication;

namespace Naovigate.Testing
{
    class GoalCommunicatorTest
    {
        private Socket server;
        private IPEndPoint endPoint;
        private NetworkStream serverStream;
        private int port;

        public GoalCommunicatorTest(int port)
        {
            this.port = port;
        }

        private void Setup()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = null;
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = ip;
                }
            }
            endPoint = new IPEndPoint(ipAddress, 11000);

            server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        }

        public void Start()
        {
            Setup();
            server.Bind(endPoint);
            server.Listen(10);
            Console.WriteLine("Run");
            new Thread(new ThreadStart(run)).Start();
            Console.WriteLine("Connect Client");
            //Stream clientStream = new NetworkStream(client);
            GoalCommunicator client = new GoalCommunicator(endPoint, 11000);
            client.Connect();
            Console.WriteLine("Client Connected");
            while (serverStream == null || client.Stream == null) ;
            runTests(new CommunicationStream(serverStream), new CommunicationStream(client.Stream));
        }

        public void run()
        {
            Console.WriteLine("Server Listen");
            Socket handler = server.Accept();
            serverStream = new NetworkStream(handler);
            Console.WriteLine("Server Ready");

            while (true)
            {
                int b = serverStream.ReadByte();
                if (b != -1) serverStream.WriteByte((byte)(0xFF & b));
            }
        }

        private void runTests(CommunicationStream server, CommunicationStream client)
        {
            testMethods(server, client);
        }

        private void testMethods(CommunicationStream server, CommunicationStream client)
        {
            int x = 432;
            client.WriteInt(x);
            client.Stream.Flush();
            Thread.Sleep(500);
            int ret = server.ReadInt();
            Debug.Assert(x == ret);
            Thread.Sleep(5000);
        }
    }
}
