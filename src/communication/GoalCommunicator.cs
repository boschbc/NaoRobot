using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;
namespace Naovigate.Communication
{
   
    public class GoalCommunicator
    {

        protected static GoalCommunicator instance = null;
        private static String defaultIp = "127.0.0.1";
        private static int defaultPort = 1337;

        private String ip;
        private int port;
        private TcpClient client;
        private NetworkStream stream;
        private IPEndPoint endPoint;
        private Dictionary<String, Action> handlers;
		
        // thread variables
        private Thread t;
        private bool running;

        /**
         * construct a new GoalCommunicator instance from the specified ip
         */
        public GoalCommunicator(String ip, int port)
        {
            
            this.ip = ip;
            this.port = port;
            init();
        }

        /**
        * construct a new GoalCommunicator instance from the specified IPEndPoint
        */
        public GoalCommunicator(IPEndPoint end, int port)
        {
            this.endPoint = end;
            this.port = port;
            init();
        }

        /**
         * initialize class objects.
         * Note: instance will be set to the newly created GoalCommunicator, there should be no other GoalCommunicator in existance
         */
        private void init()
        {
            this.running = false;
            this.handlers = new Dictionary<string, Action>();
            this.client = new TcpClient();
            Debug.Assert(instance == null);
            instance = this;
        }

        /**
         * return the GoalCommunicator instance
         */
        public static GoalCommunicator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GoalCommunicator(defaultIp, defaultPort);
                }
                return instance;
            }
        }
        /**
         * create remote end point (Goal server ip).
         */
        private void Setup()
        {
            // 
            IPAddress ipAddress = IPAddress.Parse(ip);
            endPoint = new IPEndPoint(ipAddress, port);
        }

        /**
         * Connect to the server
         */
        public void Connect()
        {
            if (endPoint == null)
            {
                Setup();
            }
           
            client.Connect(endPoint);
            stream = client.GetStream();
        }

        /**
         * start the client
         */
        public void start()
        {
            t = new Thread(new ThreadStart(Run));
            t.Start();
        }

        /*
         * MAIN
         * 
         * listen for data, respond to orders received from the server
         */
        public void Run()
        {
            while (running)
            {
                while (!stream.DataAvailable)
                {
                    // sleep
                }
                
                // wait for orders, send to listeners
            }
            Close();
        }

        /**
         * close the GoalCommunicator
         */
        public void Close()
        {
            running = false;
            if (client != null) client.Close();
        }

        /**
         * return the stream used by the client
         */
        public NetworkStream GetStream()
        {
            return stream;
        }

        /**
         * add a listener that will be notified when a request is made
         * that the listener can handle.
         */
        public void RegisterHandler(String name, Action action)
        {
            handlers.Add(name, action);
        }
    }
}
