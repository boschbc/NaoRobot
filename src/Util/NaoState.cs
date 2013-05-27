using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;

using Aldebaran.Proxies;

using Naovigate.Communication;
using Naovigate.Movement;
using Naovigate.Navigation;

namespace Naovigate.Util
{
    public class NaoState
    {
        protected static NaoState instance = null;

        protected IPAddress ip;
        protected int port;
        protected PointF location;
        protected float rotation;
        protected int batteryLeft;
        protected float temperature;

        protected List<IDisposable> proxies = new List<IDisposable>();
        protected bool connected = false;
        protected Stopwatch Stopwatch = new Stopwatch();

        protected TextToSpeechProxy speech;
        protected MotionProxy motion;
        protected BatteryProxy battery;
        protected SensorsProxy sensors;
        protected MemoryProxy memory;
        protected Map map;

        public static NaoState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NaoState();
                }
                return instance;
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
                Disconnect();
            }
            Logger.Log(this, "Connecting to Nao...");
            ip = endPoint.Address;
            port = endPoint.Port;
            connected = true;
            CreateMyProxies();
            Update();
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
            unsubscribeAll();
            ip = null;
            port = -1;
            connected = false;
            TeardownProxies();
            Logger.Log(this, "Disconnected.");
        }


        ///<summary>
        /// Creates proxes to be used by this class only.
        /// </summary>
        private void CreateMyProxies()
        {
            if (!Connected)
                return;
            motion = MotionProxy;
            battery = BatteryProxy;
            sensors = SensorsProxy;
            memory = MemoryProxy;
            speech = SpeechProxy;
        }

        /// <summary>
        /// Disconnect all the proxies used currently.
        /// </summary>
        private void TeardownProxies()
        {
            if (Connected)
                return;
            try
            {
                foreach (IDisposable d in proxies)
                {
                    d.Dispose();
                }
                proxies.Clear();
            }
            catch
            {
                throw new UnavailableConnectionException("Error while disconnecting proxies.", IP.ToString(), Port);
            }
        }

        //Unsubscribes from everything except videodeviceproxy
        private void unsubscribeAll()
        {
            LandMarkDetectionProxy landmark = LandMarkDetectionProxy;
            foreach (ArrayList sub in (ArrayList)landmark.getSubscribersInfo())
            {
                landmark.unsubscribe((String)sub[0]);
            }
            foreach (ArrayList sub in (ArrayList)sensors.getSubscribersInfo())
            {
                sensors.unsubscribe((String)sub[0]);
            }
            SonarProxy sonar = SonarProxy;
            foreach (ArrayList sub in (ArrayList)sonar.getSubscribersInfo())
            {
                sonar.unsubscribe((String)sub[0]);
            }            
        }

        /// <summary>
        /// The IP of the currently connected-to Nao.
        /// </summary>
        /// <value>The IP.</value>
        public IPAddress IP
        {
            get { return ip; }
        }

        /// <summary>
        /// The port of the currently connected-to Nao.
        /// </summary>
        /// <value>The port.</value>
        public int Port
        {
            get { return port; }
        }

        /// <summary>
        /// The location of the currently connected-to Nao.
        /// </summary>
        /// <value>The location.</value>
        public PointF Location
        {
            get { return location; }
        }

        /// <summary>
        /// The rotation angle of the currently connected-to Nao.
        /// </summary>
        /// <value>The rotation.</value>
        public float Rotation
        {
            get { return rotation; }
        }

        /// <summary>
        /// The temperature of the currently connected to nao's joints.
        /// </summary>
        /// <value>The temperature level.</value>
        public float Temperature
        {
            get { return temperature; }
        }

        /// <summary>
        /// The battery charge left of the currently connected-to Nao.
        /// </summary>
        /// <value>The battery percentage left.</value>
        public int BatteryPercentageLeft
        {
            get { return batteryLeft; }
        }

        /// <summary>
        /// The overview map of the world we are exploring.
        /// </summary>
        /// <value>The map.</value>
        public Map Map {
            get {
                return map;
            }
            set {
                map = value;
            }
        }
        
        /// <summary>
        /// Attempts to create a proxy of given type.
        /// @throws UnavailableConnectionException if proxy creation fails.
        /// </summary>
        /// <typeparam name="TProxy">The type of proxy to be created.</typeparam>
        /// <returns>A new proxy of the requested type.</returns>
        protected virtual TProxy createProxy<TProxy>() where TProxy : IDisposable
        {
            try
            {
                TProxy ret = (TProxy)Activator.CreateInstance(typeof(TProxy), IP.ToString(), Port);
                proxies.Add(ret);
                return ret;
            }
            catch
            {
                throw new UnavailableConnectionException("Could not create proxy: " + typeof(TProxy).Name);
            }
        }
        
        /// <summary>
        /// Gets the battery proxy.
        /// </summary>
        /// <value>The battery proxy.</value>
        public BatteryProxy BatteryProxy
        {
            get
            {
                BatteryProxy res = createProxy<BatteryProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets a landmark-detection proxy.
        /// </summary>
        /// <value>A landmark-detection proxy.</value>
        public LandMarkDetectionProxy LandMarkDetectionProxy
        {
            get
            {
                LandMarkDetectionProxy res = createProxy<LandMarkDetectionProxy>();
                return res;
            }
        }
        
        /// <summary>
        /// Gets a object-detection proxy.
        /// </summary>
        /// <value>A object-detection proxy.</value>
        public VisionRecognitionProxy ObjectDetectionProxy
        {
            get
            {
                VisionRecognitionProxy res = createProxy<VisionRecognitionProxy>();
                proxies.Add(res);
                return res;
            }
        }


        /// <summary>
        /// Gets a memory proxy.
        /// </summary>
        /// <value>A memory proxy.</value>
        public MemoryProxy MemoryProxy
        {
            get
            {
                MemoryProxy res = createProxy<MemoryProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets the motion proxy.
        /// </summary>
        /// <value>The motion proxy.</value>
        public virtual MotionProxy MotionProxy
        {
            get
            {
                MotionProxy res = createProxy<MotionProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets the posture proxy.
        /// </summary>
        /// <value>The posture proxy.</value>
        public virtual RobotPostureProxy PostureProxy
        {
            get
            {
                RobotPostureProxy res = createProxy<RobotPostureProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets the sensors proxy.
        /// </summary>
        /// <value>The sensors proxy.</value>
        public SensorsProxy SensorsProxy
        {
            get
            {
                SensorsProxy res = createProxy<SensorsProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets the sonar proxy.
        /// </summary>
        /// <value>The sonar proxy.</value>
        public SonarProxy SonarProxy
        {
            get
            {
                SonarProxy res = createProxy<SonarProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets the speech proxy.
        /// </summary>
        /// <value>The speech proxy.</value>
        public TextToSpeechProxy SpeechProxy
        {
            get
            {
                TextToSpeechProxy res = createProxy<TextToSpeechProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets the video proxy.
        /// </summary>
        /// <value>The video proxy.</value>
        public VideoDeviceProxy VideoProxy
        {
            get
            {
                VideoDeviceProxy res = createProxy<VideoDeviceProxy>();
                return res;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Naovigate.Util.NaoState"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool Connected
        {
            get { return connected; }
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
        public void Update()
        {
            if (!Connected)
                throw new UnavailableConnectionException("Attempted to update state while not connected.", ip.ToString(), port);

            try
            {
                List<float> vector = motion.getRobotPosition(false);
                location = new PointF(vector[0], vector[1]);
                rotation = vector[2];  
                batteryLeft = battery.getBatteryCharge();
                temperature = (float) memory.getData("Device/SubDeviceList/Battery/Temperature/Sensor/Value");
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
