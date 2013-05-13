using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Naovigate.Communication
{
    public class GoalCommunicator : IDisposable
    {
        protected static GoalCommunicator instance = null;
        private static String defaultIp = "127.0.0.1";
        private static int defaultPort = 1337;

        private IPAddress ip;
        private int port;
        private TcpClient client;
        private CommunicationStream coms;
        private NetworkStream stream;
        private IPEndPoint endPoint;
        private Dictionary<String, Action> handlers;
		
        // thread variables
        private bool running;
        private byte[] receiveBuffer;

        private GoalCommunicator()
        {
            this.running = false;
            this.handlers = new Dictionary<string, Action>();
            this.client = new TcpClient();
            this.receiveBuffer = new byte[this.client.ReceiveBufferSize];
            instance = this;
        }

        /**
         * construct a new GoalCommunicator instance from the specified ip
         */
        public GoalCommunicator(String ip, int port) : this()
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
            this.endPoint = new IPEndPoint(this.ip, port);
        }

        /**
         * construct a new GoalCommunicator instance from the specified IPEndPoint
         */
        public GoalCommunicator(IPEndPoint end, int port) : this()
        {
            if (end == null)
            {
                throw new ArgumentNullException();
            }
            this.ip = end.Address;
            this.port = port;
            this.endPoint = end;
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
         * Connect to the server
         */
        public void Connect()
        {
            this.client.Connect(this.endPoint);
            this.stream = this.client.GetStream();
            this.coms = new CommunicationStream(this.stream);
        }

        /**
         * Start the main loop.
         */
        public void Start()
        {
            if (!this.client.Connected)
            {
                this.Connect();
            }
            this.running = true;
            this.stream.BeginRead(this.receiveBuffer, 0, this.receiveBuffer.Length, this.OnData, null);
        }

        public void Stop()
        {
            this.running = false;
            this.stream.Close();
        }

        /*
         * MAIN
         * 
         * listen for data, respond to orders received from the server
         */
        private void OnData(IAsyncResult result)
        {
            if (!this.running)
            {
                return;
            }
            this.stream.EndRead(result);

            // Decode receive buffer.
            String data = new UTF8Encoding().GetString(this.receiveBuffer);

            // Fire event handlers.
            foreach (var entry in this.handlers)
            {
                String actionName = entry.Key;
                Action handler = entry.Value;

                if (!data.StartsWith(actionName))
                {
                    continue;
                }
                handler();
            }

            // Begin reading again.
            this.Start();
        }

        /**
         * close the GoalCommunicator
         */
        public void Dispose()
        {
            this.running = false;
            if (this.client != null) {
                this.client.Close();
            }
        }

        /**
         * returns stream
         */
        public virtual NetworkStream Stream {
            get { return this.stream; }
        }

        /**
        * returns stream
        */
        public virtual CommunicationStream Coms
        {
            get { return this.coms; }
        }

        /**
         * returns ip
         */
        public IPAddress IP {
            get { return this.ip; }
        }

        /**
         * returns port
         */
        public int Port {
            get { return this.port; }
        }

        /**
         * returns bool running
         */
        public bool IsRunning {
            get { return this.running; }
        }

        /**
         * add a listener that will be notified when a request is made
         * that the listener can handle.
         */
        public virtual void RegisterHandler(String name, Action action)
        {
            handlers.Add(name, action);
        }
    }
}
