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
    public static class NaoState
    {
        private static IPAddress ip;
        private static int port;
        private static PointF location;
        private static float rotation;
        private static int batteryLeft;
        private static float temperature;

        private static List<IDisposable> proxies = new List<IDisposable>();
        private static bool connected = false;
        private static Stopwatch Stopwatch = new Stopwatch();

        private static TextToSpeechProxy speech;
        private static MotionProxy motion;
        private static BatteryProxy battery;
        private static SensorsProxy sensors;
        private static MemoryProxy memory;

        /// <summary>
        /// Connect to a Nao. Will disconnect from any already connected-to Nao.
        /// </summary>
        /// <param name="ip">IP to connect to.</param>
        /// <param name="port">Port to connect to.</param>
        public static void Connect(string ip, int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Connect(endPoint);
        }

        /// <summary>
        /// Connect to a Nao. Will disconnect from any already connected-to Nao.
        /// </summary>
        /// <param name="endPoint">IP end point to connect to.</param>
        public static void Connect(IPEndPoint endPoint)
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
        public static void Disconnect()
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
        private static void CreateMyProxies()
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
        private static void TeardownProxies()
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
                throw new UnavailableConnectionException("Error while disconnecting proxies.", IP.ToString(), port);
            }
        }

        /// <summary>
        /// The IP of the currently connected-to Nao.
        /// </summary>
        /// <value>The IP.</value>
        public static IPAddress IP
        {
            get { return ip; }
        }

        /// <summary>
        /// The port of the currently connected-to Nao.
        /// </summary>
        /// <value>The port.</value>
        public static int Port
        {
            get { return port; }
        }

        /// <summary>
        /// The location of the currently connected-to Nao.
        /// </summary>
        /// <value>The location.</value>
        public static PointF Location
        {
            get { return location; }
        }

        /// <summary>
        /// The rotation angle of the currently connected-to Nao.
        /// </summary>
        /// <value>The rotation.</value>
        public static float Rotation
        {
            get { return rotation; }
        }

        /// <summary>
        /// The temperature of the currently connected to nao's joints.
        /// </summary>
        /// <value>The temperature level.</value>
        public static float Temperature
        {
            get { return temperature; }
        }

        /// <summary>
        /// The battery charge left of the currently connected-to Nao.
        /// </summary>
        /// <value>The battery percentage left.</value>
        public static int BatteryPercentageLeft
        {
            get { return batteryLeft; }
        }

        /// <summary>
        /// Gets the motion proxy.
        /// </summary>
        /// <value>The motion proxy.</value>
        public static MotionProxy MotionProxy
        {
            get
            {
                MotionProxy res = new MotionProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        /// <summary>
        /// Gets the speech proxy.
        /// </summary>
        /// <value>The speech proxy.</value>
        public static TextToSpeechProxy SpeechProxy
        {
            get
            {
                TextToSpeechProxy res = new TextToSpeechProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        /// <summary>
        /// Gets the posture proxy.
        /// </summary>
        /// <value>The posture proxy.</value>
        public static RobotPostureProxy PostureProxy
        {
            get
            {
                RobotPostureProxy res = new RobotPostureProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        /// <summary>
        /// Gets the sensors proxy.
        /// </summary>
        /// <value>The sensors proxy.</value>
        public static SensorsProxy SensorsProxy
        {
            get
            {
                SensorsProxy res = new SensorsProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        /// <summary>
        /// Gets the video proxy.
        /// </summary>
        /// <value>The video proxy.</value>
        public static VideoDeviceProxy VideoProxy
        {
            get
            {
                VideoDeviceProxy res = new VideoDeviceProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        /// <summary>
        /// Gets the battery proxy.
        /// </summary>
        /// <value>The battery proxy.</value>
        public static BatteryProxy BatteryProxy
        {
            get
            {
                BatteryProxy res = new BatteryProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        /// <summary>
        /// Gets the sonar proxy.
        /// </summary>
        /// <value>The sonar proxy.</value>
        public static SonarProxy SonarProxy
        {
            get
            {
                SonarProxy res = new SonarProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        public static MemoryProxy MemoryProxy
        {
            get
            {
                MemoryProxy res = new MemoryProxy(ip.ToString(), port);
                proxies.Add(res);
                return res;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Naovigate.Util.NaoState"/> is connected.
        /// </summary>
        /// <value><c>true</c> if connected; otherwise, <c>false</c>.</value>
        public static bool Connected
        {
            get
            {
                return connected;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Naovigate.Util.NaoState"/>'s data is out of date.
        /// </summary>
        /// <value><c>true</c> if out of date; otherwise, <c>false</c>.</value>
        public static bool OutOfDate(int threshold)
        {
            return Stopwatch.ElapsedMilliseconds > threshold;
        }

        /// <summary>
        /// Update this NaoState with new values retrieved from the Nao. This includes battery charge, robot position,
        /// robot rotation, et cetera.
        /// </summary>
        public static void Update()
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
