using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Naovigate.Event;

namespace Naovigate.Communication
{
    public class GoalCommunicator : IDisposable
    {
        protected static GoalCommunicator instance = null;
        public static readonly String defaultIp = "127.0.0.1";
        public static readonly int defaultPort = 6747;

        private IPAddress ip;
        private int port;
        private TcpClient client;
        private CommunicationStream coms;
        private NetworkStream stream;
        private IPEndPoint endPoint;
		
        // thread variables
        private bool running;

        private GoalCommunicator()
        {
            this.running = false;
            this.client = new TcpClient();
            instance = this;
        }

        /*
         * construct a new GoalCommunicator instance from the specified ip
         */
        public GoalCommunicator(String ip, int port) : this()
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
            this.endPoint = new IPEndPoint(this.ip, port);
        }

        /*
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

        /*
         * return the GoalCommunicator instance
         */
        public static GoalCommunicator Instance
        {
            get { return instance == null ? instance = new GoalCommunicator(defaultIp, defaultPort) : instance; }
        }

        /*
         * Connect to the server
         */
        public void Connect()
        {
            this.client.Connect(this.endPoint);
            this.stream = this.client.GetStream();
            this.coms = new CommunicationStream(this.stream);
        }

        /*
         * Start the main loop.
         */
        public void Start()
        {
            // connect if it is not connected yet.
            if (!this.client.Connected)
            {
                this.Connect();
            }
            while (IsRunning)
            {
                // the EventCode
                byte code = coms.ReadByte();
                try
                {
                    // create the event
                    INaoEvent e = NaoEventFactory.NewEvent(code);
                    EventQueue.Instance.Post(e);
                }
                catch
                {
                    Console.WriteLine("InvalidActionCode: "+CommunicationStream.ToBitString(code));
                }
            }
        }

        public void Stop()
        {
            this.running = false;
            this.stream.Close();
        }

        /*
         * close the GoalCommunicator
         */
        public void Dispose()
        {
            this.running = false;
            if (this.client != null) {
                this.client.Close();
            }
        }

        /*
         * returns stream
         */
        public virtual NetworkStream Stream {
            get { return this.stream; }
        }

        /*
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

        /*
         * returns port
         */
        public int Port {
            get { return this.port; }
        }

        /*
         * returns bool running
         */
        public bool IsRunning {
            get { return this.running; }
        }
    }
}
