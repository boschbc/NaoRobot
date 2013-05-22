using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.IO;

using Aldebaran.Proxies;
using Naovigate.Movement;
using Naovigate.Communication;

namespace Naovigate.Util
{
    public class NaoState
    {
        private static NaoState instance = null;

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
            ip = endPoint.Address;
            port = endPoint.Port;
            connected = true;
            CreateMyProxies();
            Update();
        }

        /// <summary>
        /// Disconnect from the currently connected-to Nao. Will do nothing if not connected.
        /// </summary>
        public virtual void Disconnect()
        {
            if (Connected) {
                return;
            }
            ip = null;
            port = -1;
            connected = false;
            TeardownProxies();
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
        /// Attempts to create a proxy of given type.
        /// @throws UnavailableConnectionException if proxy creation fails.
        /// </summary>
        /// <typeparam name="TProxy"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        protected virtual TProxy createProxy<TProxy>(Func<string, int, TProxy> proxy)
        {
            try
            {
                return proxy(IP.ToString(), Port);
            }
            catch
            {
                throw new UnavailableConnectionException("Could not create proxy: " + typeof(TProxy).Name, IP.ToString(), Port);
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
                BatteryProxy res = createProxy<BatteryProxy>((ip, port) => new BatteryProxy(ip, port));
                proxies.Add(res);
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
                LandMarkDetectionProxy res = createProxy<LandMarkDetectionProxy>((ip, port) => new LandMarkDetectionProxy(ip, port));
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
                MemoryProxy res = createProxy<MemoryProxy>((ip, port) => new MemoryProxy(ip, port));
                proxies.Add(res);
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
                MotionProxy res = createProxy<MotionProxy>((ip, port) => new MotionProxy(ip, port));
                proxies.Add(res);
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
                RobotPostureProxy res = createProxy<RobotPostureProxy>((ip, port) => new RobotPostureProxy(ip, port));
                proxies.Add(res);
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
                SensorsProxy res = createProxy<SensorsProxy>((ip, port) => new SensorsProxy(ip, port));
                proxies.Add(res);
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
                SonarProxy res = createProxy<SonarProxy>((ip, port) => new SonarProxy(ip, port));
                proxies.Add(res);
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
                TextToSpeechProxy res = createProxy<TextToSpeechProxy>((ip, port) => new TextToSpeechProxy(ip, port));
                proxies.Add(res);
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
                VideoDeviceProxy res = createProxy<VideoDeviceProxy>((ip, port) => new VideoDeviceProxy(ip, port));
                proxies.Add(res);
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
                Console.WriteLine("Failed Nao update: " + e);
            }

            // Count the time between this update and the next.
            Stopwatch.Restart();
        }
    }    
}
