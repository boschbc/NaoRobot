using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using Aldebaran.Proxies;

using Naovigate.Communication;
using Naovigate.Grabbing;
using Naovigate.Navigation;
using Naovigate.Event;
using Naovigate.Movement;

namespace Naovigate.Util
{
    public class NaoState
    {
        public event Action<String, Int32> OnConnect;
        public event Action<String, Int32> OnDisconnect;
        
        protected static NaoState instance = null;

        protected bool connected = false;
        protected Stopwatch Stopwatch = new Stopwatch();
        protected bool idle = true;
        protected MotionProxy motion;
        protected BatteryProxy battery;
        protected MemoryProxy memory;

        public NaoState()
        {
            try
            {
                Map = MapParser.Parse("../resources/maps/testmaze.map");
            }
            catch
            {
                Logger.Log(this, "Map parsing failed: Exit");
                Environment.Exit(-1);
            }
        }

        public static NaoState Instance
        {
            get
            {
                return instance == null ? instance = new NaoState() : instance;
            }
            set { instance = value; }
        }

        /// <summary>
        /// Connect to a Nao. Will disconnect from any already connected-to Nao.
        /// </summary>
        /// <param name="ip">IP to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        public virtual void Connect(string ip, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Connect(endPoint);
        }

        /// <summary>
        /// Connect to a Nao. Will disconnect from any already connected-to Nao.
        /// </summary>
        /// <param name="endPoint">IP end point to connect to.</param>
        public virtual void Connect(IPEndPoint endPoint)
        {
            if (Connected) {
                Logger.Log(this, "Already Connected");
                return;
            }
            Logger.Log(this, "Connecting to Nao...");
            IP = endPoint.Address;
            Port = endPoint.Port;
            CreateMyProxies();
            connected = true;
            Update();
            if (OnConnect != null)
                OnConnect(IP.ToString(), Port);
            Logger.Log(this, "Connection established.");
        }

        /// <summary>
        /// Disconnect from the currently connected-to Nao. Will do nothing if not connected.
        /// </summary>
        public virtual void Disconnect()
        {
            if (!Connected) {
                return;
            }
            Logger.Log(this, "Disconnecting from Nao...");
            if (!Proxies.UnsubscribeAll())
                Logger.Log(this, "Can't unsubscribe. But that's OK if you're using WeBots.");
            IP = null;
            Port = -1;
            connected = false;
            Proxies.DisposeAllProxies();
            if (OnDisconnect != null)
                OnDisconnect(IP.ToString(), Port);
            Logger.Log(this, "Disconnected.");
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Naovigate.Util.NaoState"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool Connected
        {
            get{
                if (IP == null) connected = false;
                return connected; 
            }
        }

        ///<summary>
        /// Creates proxes to be used by this class only.
        /// </summary>
        private void CreateMyProxies()
        {
            motion = Proxies.GetProxy<MotionProxy>();
            battery = Proxies.GetProxy<BatteryProxy>();
            memory = Proxies.GetProxy<MemoryProxy>();
        }        

        /// <summary>
        /// The IP of the currently connected-to Nao.
        /// </summary>
        /// <value>The IP.</value>
        public IPAddress IP
        {
            get;
            private set;
        }

        /// <summary>
        /// The port of the currently connected-to Nao.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get;
            private set;
        }

        /// <summary>
        /// The location of the currently connected-to Nao.
        /// </summary>
        /// <value>The location.</value>
        public PointF Location
        {
            get;
            private set;
        }

        /// <summary>
        /// The rotation angle of the currently connected-to Nao.
        /// </summary>
        /// <value>The rotation.</value>
        public float Rotation
        {
            get;
            private set;
        }

        /// <summary>
        /// The temperature of the currently connected to nao's joints.
        /// </summary>
        /// <value>The temperature level.</value>
        public float Temperature
        {
            get;
            private set;
        }

        /// <summary>
        /// The battery charge left of the currently connected-to Nao.
        /// </summary>
        /// <value>The battery percentage left.</value>
        public int BatteryPercentageLeft
        {
            get;
            private set;
        }

        /// <summary>
        /// value indicating if the Nao is idle.
        /// </summary>
        /// <value>The location.</value>
        public bool Idle
        {
            get { return idle; }
            private set {
                if (idle != value)
                {
                    Logger.Log(this, "State: "+(value ? 0 : 1));
                    // value changed, update goal.
                    EventQueue.Goal.Post(new Event.NaoToGoal.StateEvent(value ? 0 : 1));
                }
                idle = value;
            }
        }

        /// <summary>
        /// The overview map of the world we are exploring.
        /// </summary>
        /// <value>The map.</value>
        public Map Map
        {
            get;
            set;
        }

        public bool HoldingObject
        {
            get
            {
                return Grabber.Instance.HoldingObject();
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Naovigate.Util.NaoState"/>'s data is out of date.
        /// </summary>
        /// <value><c>true</c> if out of date; otherwise, <c>false</c>.</value>
        public bool OutOfDate(int threshold)
        {
            return Stopwatch.ElapsedMilliseconds > threshold;
        }

        /// <summary>
        /// Update this NaoState with new values retrieved from the Nao. This includes battery charge, robot position,
        /// robot rotation, et cetera.
        /// </summary>
        /// <exception cref="UnavailableConnectionException">NaoState is not connected to a Nao.</exception>
        public void Update()
        {
            try
            {
                List<float> vector = motion.getRobotPosition(true);
                Location = new PointF(vector[0], vector[1]);
                Rotation = vector[2];
                BatteryPercentageLeft = battery.getBatteryCharge();
                Temperature = (float) memory.getData("Device/SubDeviceList/Battery/Temperature/Sensor/Value");

                // update idle
                Idle = !Walk.Instance.IsMoving() && EventQueue.Nao.Current == null;                
            }
            catch(Exception e)
            {
                Logger.Log(this, "Failed Update(). Unknown exception occurred: " + e.ToString());
            }

            // Count the time between this update and the next.
            Stopwatch.Restart();
        }
    }    
}
