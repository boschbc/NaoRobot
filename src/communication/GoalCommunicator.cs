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
    public class GoalCommunicator : IDisposable
    {
        public static readonly String DefaultIP = "127.0.0.1";
        public static readonly int DefaultPort = 6747;

        protected static GoalCommunicator instance = null;
        
        protected IPAddress ip;
        protected int port;
        protected TcpClient client;
        protected ICommunicationStream communicationStream;
        protected NetworkStream stream;
        protected IPEndPoint endPoint;
        protected volatile bool running;
        protected int reconnectAttempCount;

        private GoalCommunicator()
        {
            this.running = false;
            reconnectAttempCount = 0;
            this.client = new TcpClient();
            instance = this;
        }

        /// <summary>
        /// Construct a new GoalCommunicator instance from the specified IP.
        /// Throws FormatException if the given IP is wrongly formatted.
        /// </summary>
        /// <param name="ip">String containing an IP.</param>
        /// <param name="port">Port number.</param>
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
        /// The GoalCommunicator instance.
        /// </summary>
        public static GoalCommunicator Instance
        {
            get { return instance == null ? instance = new GoalCommunicator(MainProgram.goalIP, DefaultPort) : instance; }
        }

        /// <summary>
        /// Establish connection to the server.
        /// </summary>
        public bool Connect()
        {
            try
            {
                Logger.Log(this, "Connecting to: " + this.ip.ToString() + ":" + this.port.ToString());
                client.Connect(this.endPoint);
                stream = this.client.GetStream();
                if (communicationStream == null) communicationStream = new BitStringCommunicationStream(stream);
                else communicationStream.Stream = stream;
                Logger.Log(this, "Connection established.");
                EventQueue.Goal.Post(new AgentEvent());
            } catch{
                Logger.Log(this, "Connection could not be established, is the server running?");
                return false;
            }
            return true;
        }

        /// <summary>
        /// attempt to reconnect a number of times.
        /// </summary>
        /// <param name="attempts"></param>
        /// <returns></returns>
        private bool Reconnect(int attempts)
        {
            bool res = false;
            for (int i = 0; i < attempts && !(res = Reconnect()); i++) ;
            return attempts == 0 ? false : res;
        }

        /// <summary>
        /// reconnect the GoalCommunicator after a connection lost.
        /// the calling thread will sleep for some time, depending on the attemps that were made.
        /// </summary>
        /// <param name="attempt"></param>
        /// <returns>true if the connection was restored, false otherwise.</returns>
        private bool Reconnect()
        {
            reconnectAttempCount++;
            Logger.Log(this, "Reconnecting... attempt " + reconnectAttempCount + ", timeout = " + (1000 * (1 << reconnectAttempCount)));
            
            client = new TcpClient();
            bool res = Connect();
            Thread.Sleep(500 * (1 << reconnectAttempCount));
            if (res) reconnectAttempCount = 0; 
            return res;
        }

        /// <summary>
        /// Start the main loop in another thread.
        /// </summary>
        public void StartAsync()
        {
            new Thread(new ThreadStart(Start)).Start();
        }

        /// <summary>
        /// Start the main loop.
        /// </summary>
        public void Start()
        {
            if (this.client.Connected || Connect())
            {
                Logger.Log(this, "Entering main loop.");
                running = true;
                while (IsRunning)
                {   //connection lost, attempt to reconnect three times
                    if (!communicationStream.Open && !Reconnect(3))
                    {
                        throw new UnavailableConnectionException();
                    }
                    byte code = 0;
                    try
                    {
                        code = communicationStream.ReadByte();
                        INaoEvent naoEvent = NaoEventFactory.NewEvent(code);
                        EventQueue.Nao.Post(naoEvent);
                    }
                    catch (IOException e)  //Communication Stream got closed.
                    {
                        Logger.Log(this, "Communication stream closed: " + e.Message);
                        communicationStream.Open = false;
                    }
                    catch (InvalidEventCodeException)  //Received invalid event code.
                    {
                        Logger.Log(this, "Invalid event code received: " + code);
                        INaoEvent failureEvent = new FailureEvent(code);
                        EventQueue.Goal.Post(failureEvent);
                    }
                    catch (Exception e)
                    {
                        Logger.Log(this, "Unexpected exception occurred while processing incoming data: " + e.ToString());
                    }
                }
                Dispose();
                Logger.Log(this, "Exiting main loop.");
            }
            else
            {
                throw new UnavailableConnectionException();
            }
        }

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public void Close()
        {
            Logger.Log(this, "Disconnecting from server...");
            this.running = false;
            if(communicationStream != null) communicationStream.Close();
            Logger.Log(this, "Disconnected.");
        }

        /// <summary>
        /// Terminates the communicator and performs clean-up.
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// The internal network stream.
        /// </summary>
        public virtual NetworkStream Stream {
            get { return this.stream; }
        }

        /// <summary>
        /// The internal communication stream.
        /// </summary>
        public virtual ICommunicationStream Coms
        {
            get { return this.communicationStream; }
        }

        /// <summary>
        /// The connection's IP address.
        /// </summary>
        public IPAddress IP {
            get { return this.ip; }
        }

        /// <summary>
        /// The connection's port number.
        /// </summary>
        public int Port {
            get { return this.port; }
        }

        /// <summary>
        /// The communicator's state.
        /// </summary>
        public bool IsRunning {
            get { return this.running; }
        }
    }
}
