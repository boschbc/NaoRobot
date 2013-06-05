using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using Aldebaran.Proxies;

using Naovigate.Communication;
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

        protected ISet<IDisposable> proxies = new HashSet<IDisposable>();
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
                Logger.Log(this, "Already Connected");
                return;
            }
            Logger.Log(this, "Connecting to Nao...");
            connected = true;
            ip = endPoint.Address;
            port = endPoint.Port;
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
            try
            {
                UnsubscribeAll();
            }
            catch (UnavailableConnectionException)
            {
                Logger.Log(this, "Can't unsubscribe. But that's OK if you're using WeBots.");
            }
            ip = null;
            port = -1;
            connected = false;
            TeardownProxies();
            Logger.Log(this, "Disconnected.");
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Naovigate.Util.NaoState"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public bool Connected
        {
            get
            {
                if (IP == null)
                    return false;
                try
                {
                    MotionProxy motion = new MotionProxy(IP.ToString(), Port);
                    motion.Dispose();
                    return connected;
                }
                catch
                {
                    return false;
                }
            }
        }

        ///<summary>
        /// Creates proxes to be used by this class only.
        /// </summary>
        private void CreateMyProxies()
        {
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
                    Logger.Log("Dispose " + d);
                    d.Dispose();
                }
            }
            catch
            {
                proxies.Clear();
                throw new UnavailableConnectionException("Error while disconnecting proxies.", IP.ToString(), Port);
            }
            proxies.Clear();
        }

        /// <summary>
        /// Unsubscribes from any landmark-detection proxies.
        /// </summary>
        private void UnsubscribeLandMarkProxies()
        {
            LandMarkDetectionProxy landmark = LandMarkDetectionProxy;
            foreach (ArrayList sub in (ArrayList)landmark.getSubscribersInfo())
            {
                landmark.unsubscribe(sub[0].ToString());
            }
        }

        /// <summary>
        /// Unsubscribes from any sensor proxies.
        /// </summary>
        private void UnsubscribeSensorProxies()
        {
            if (!Connected)
                return;
            foreach (ArrayList sub in (ArrayList)sensors.getSubscribersInfo())
            {
                sensors.unsubscribe(sub[0].ToString());
            }
        }

        /// <summary>
        /// Unsubscribes from any sonar proxies.
        /// </summary>
        private void UnsubscribeSonarProxies()
        {
            SonarProxy sonar = SonarProxy;
            foreach (ArrayList sub in (ArrayList)sonar.getSubscribersInfo())
            {
                sonar.unsubscribe(sub[0].ToString());
            }
        }

        /// <summary>
        /// Unsubscribes all instances of LandMarkDetectionProxy, SensorsProxy and SonarProxy.
        /// </summary>
        /// <returns>Whether an error occurred and some proxies could not be unsubbed.</returns>
        private bool UnsubscribeAll()
        {
            bool noError = true;
            Action[] unsubscribers = new Action[3] 
            { 
                UnsubscribeLandMarkProxies, 
                UnsubscribeSensorProxies, 
                UnsubscribeSonarProxies 
            };

            foreach (Action unsub in unsubscribers)
            {
                try
                {
                    unsub();
                }
                catch (UnavailableConnectionException)
                {
                    noError = false;
                }
            }
            return noError;
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
        /// </summary>
        /// <typeparam name="TProxy">The type of proxy to be created.</typeparam>
        /// <returns>A new proxy of the requested type.</returns>
        /// <exception cref="UnavailableConnectionException">Proxy creation failed.</exception>
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
                return createProxy<BatteryProxy>();
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
                return createProxy<LandMarkDetectionProxy>();
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
                return createProxy<VisionRecognitionProxy>();
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
                return createProxy<MemoryProxy>();
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
                return createProxy<MotionProxy>();
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
                return createProxy<RobotPostureProxy>();
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
                return createProxy<SensorsProxy>();
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
                return createProxy<SonarProxy>();
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
                return createProxy<TextToSpeechProxy>();
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
                return createProxy<VideoDeviceProxy>();
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
