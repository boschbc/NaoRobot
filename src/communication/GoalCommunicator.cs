using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Naovigate.Event;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

namespace Naovigate.Communication
{
    /// <summary>
    /// A class that establishes and maintains a TCP connection to the GOAL-server.
    /// </summary>
    public class GoalCommunicator
    {
        public static readonly String DefaultIP = MainProgram.LocalHost;
        public static readonly int DefaultPort = MainProgram.GoalPort;

        protected static GoalCommunicator instance = null;

        private object sLock = new object();

        protected IPAddress ip;
        protected int port;
        protected TcpClient client;
        protected ICommunicationStream communicationStream;
        protected NetworkStream stream;
        protected IPEndPoint endPoint;
        private KeepAlive keepAlive;
        private int reconnectAttempCount;

        /// <summary>
        /// The GoalCommunicator instance.
        /// </summary>
        public static GoalCommunicator Instance
        {
            get { return instance == null ? instance = new GoalCommunicator(MainProgram.GoalIP, DefaultPort) : instance; }
            set { instance = value; }
        }

        /// <summary>
        /// Creates a new communicator instance.
        /// </summary>
        private GoalCommunicator()
        {
            Running = false;
            reconnectAttempCount = 0;
            client = new TcpClient();
        }

        /// <summary>
        /// Construct a new GoalCommunicator instance from the specified IP.
        /// Throws FormatException if the given IP is wrongly formatted.
        /// </summary>
        /// <param name="ip">String containing an IP.</param>
        /// <param name="port">Port number.</param>
        /// <exception cref="FormatException">The given string does not contain a properly formatted IP.</exception>
        public GoalCommunicator(String ip, int port) : this()
        {
            this.ip = IPAddress.Parse(ip);
            this.port = port;
            this.endPoint = new IPEndPoint(this.ip, port);
        }

        /// <summary>
        /// Construct a new GoalCommunicator instance from the specified IPEndPoint.
        /// </summary>
        /// <param name="end">An IPEndPoint</param>
        /// <param name="port">Port number.</param>
        public GoalCommunicator(IPEndPoint end, int port) : this()
        {
            if (end == null)
                throw new ArgumentNullException("The given IPEndPoint is null.");
            
            this.ip = end.Address;
            this.port = port;
            this.endPoint = end;
        }

        /// <summary>
        /// Establish connection to the server.
        /// </summary>
        /// <returns>True if connected successfully.</returns>
        public bool Connect()
        {
            if (Running && reconnectAttempCount == 0)
                return false;
            try
            {
                Logger.Log(this, "Connecting to: " + this.ip.ToString() + ":" + this.port.ToString());
                client.Connect(this.endPoint);
                stream = this.client.GetStream();
                if (communicationStream == null)
                    communicationStream = new BitStringCommunicationStream(stream);
                else communicationStream.InternalStream = stream;

                if (keepAlive == null) {
                    keepAlive = new KeepAlive(communicationStream);
                    //keepAlive.StartAync();
                }
                Logger.Log(this, "Connection established.");
                //send our agent id.
                if (NaoState.Instance.Connected)
                {
                    EventQueue.Goal.Post(new AgentEvent());
                    EventQueue.Goal.Post(new LocationEvent(6));
                }
                return true;
            } 
            catch (SocketException)
            {
                Logger.Log(this, "Connection could not be established, is the server running?");
                return false;
            }
        }

        /// <summary>
        /// Attempt to reconnect a given number of times.
        /// </summary>
        /// <param name="attempts">The number of times to attempt reconnection.</param>
        /// <returns>true if the connection was restored, false otherwise.</returns>
        private bool Reconnect(int attempts)
        {
            bool success = false;
            for (int i = 0; i < attempts && !(success = Reconnect()); i++) ;
            return success;
        }

        /// <summary>
        /// Reconnect to the server.
        /// The calling thread will sleep for some time, depending on the attemps that were made.
        /// </summary>
        /// <returns>true if the connection was restored, false otherwise.</returns>
        private bool Reconnect()
        {
            reconnectAttempCount++;
            int timeout = 500 * (1 << reconnectAttempCount);
            Logger.Log(this, "Reconnecting... attempt " + reconnectAttempCount +
                             ", timeout = " + timeout);
            client = new TcpClient();
            bool success = Connect();
            Thread.Sleep(timeout);
            if (success)
                reconnectAttempCount = 0; 
            return success;
        }

        /// <summary>
        /// Attempts to read incoming data from the server.
        /// </summary>
        private void ReceiveData()
        {
            if (!Running)
                return;
            byte code = 0;
            try
            {
                code = communicationStream.ReadByte();
                if (!Running)
                    return;
                INaoEvent naoEvent = NaoEventFactory.NewEvent(code);
                EventQueue.Nao.Post(naoEvent);
            }
            catch (InvalidEventCodeException e)
            {
                Logger.Except(e);
                Logger.Log(this, "Invalid event code received: " + code);
                INaoEvent failureEvent = new FailureEvent(code);
                EventQueue.Goal.Post(failureEvent);
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException || e is IOException)
                {
                    Logger.Log(this, "Communication stream got closed.");
                    communicationStream.Open = false;
                }
                else Logger.Log(this, "Unexpected exception occurred while processing incoming data: " + e);
            }
        }

        /// <summary>
        /// Start the main loop in another thread.
        /// </summary>
        public void StartAsync()
        {
            Thread t = new Thread(new ThreadStart(Start));
            t.Name = "GoalCommunicator";
            t.Start();
        }

        /// <summary>
        /// Start the main loop.
        /// </summary>
        /// <exception cref="UnavailableConnectionException">Could not connect to server.</exception>
        public void Start()
        {
            if (this.client.Connected || Connect())
            {
                Logger.Log(this, "Entering main loop.");
                Running = true;
                while (Running)
                {   
                    if (!communicationStream.Open && !Reconnect(3))
                    {
                        throw new UnavailableConnectionException("Connection to server has been lost.",
                            IP.ToString(), Port);
                    }
                    if (stream.DataAvailable)
                        ReceiveData();
                    else
                        Thread.Sleep(1);
                }
                Logger.Log(this, "Exiting main loop.");
            }
            else
            {
                throw new UnavailableConnectionException("Could not connect to the server.", IP, Port);
            }
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public void Close()
        {
            if (!Running)
                return;
            Logger.Log(this, "Disconnecting from server...");
            if(communicationStream != null)
                communicationStream.Close();
            if (client != null)
                client.Close();
            Running = false;
            Logger.Log(this, "Disconnected.");
        }

        /// <summary>
        /// The internal communication stream.
        /// </summary>
        public virtual ICommunicationStream Stream
        {
            get
            {
                return communicationStream;
            }
        }

        /// <summary>
        /// The connection's IP address.
        /// </summary>
        public IPAddress IP
        {
            get { return ip; }
        }

        /// <summary>
        /// The connection's port number.
        /// </summary>
        public int Port 
        {
            get { return port; }
        }

        /// <summary>
        /// The communicator's state.
        /// </summary>
        public bool Running
        {
            get;
            private set;
        }
    }
}
