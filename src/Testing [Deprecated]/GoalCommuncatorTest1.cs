using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Naovigate.Testing
{
    class GoalCommunicatorTest1
    {
        //private Socket socket;
        //private IPEndPoint endPoint;
        //private int port;

        public GoalCommunicatorTest1(String[] args)
        {
            Console.WriteLine("Command Line Arguments:");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }
            Console.WriteLine("Program Start");
            Console.WriteLine("Starting GoalCommunicator");
            Communication.GoalCommunicator coms = new Communication.GoalCommunicator("145.94.47.97", 9559);
            Console.WriteLine("Connect");
            coms.Connect();
            Console.WriteLine("GoalCommunicator ready");

            
            Thread.Sleep(1000);
            Console.WriteLine("Closing");
        }

        /*
        public void GoalCommunicatorTest1(int port)
        {
            this.port = port;
        }

        public void Setup()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            endPoint = new IPEndPoint(ipAddress, 11000);
            //socket = new Socket();
        }

        public void Start()
        {
            Setup();
            

        }
         * */
    }
}
