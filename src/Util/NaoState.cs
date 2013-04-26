using System;
using System.Drawing;
using System.Collections.Generic;

using Aldebaran.Proxies;

using Naovigate.Movement;

namespace Naovigate.Util
{
    public static class NaoState
    {
        private static string ip;
        private static int port;
        private static PointF location;
        private static float rotation;

        private static MotionProxy motionProxy;
        private static RobotPostureProxy postureProxy;

        private static bool connected = false;

        /**
         * Connect to a Nao.
         * @param ip_ = IP address of target Nao.
         * @param port_ = Integer of the desired port.
         **/
        public static void ConnectTo(string ip_, int port_)
        {
            ip = ip_;
            port = port_;
            CreateProxies();
            SetConnected(true);
            Update();
        }

        private static void SetConnected(bool value)
        {
            connected = value;
        }


        /**
         * Create proxies for the Nao this class is connected to.
         * @throws UnavailableConnectionException if connection is unavailable.
         **/
        private static void CreateProxies()
        {
            try
            {
                motionProxy = new MotionProxy(GetIP(), GetPort());
                postureProxy = new RobotPostureProxy(GetIP(), GetPort());
                Walk.GetInstance().RefreshProxies();
            }
            catch
            {
                throw new Exception("Connection unavailable.");
                //throw new UnavailableConnectionException();
            }
        }

        /**
        * Delete proxies for the Nao this class is connected to.
        * @throws UnavailableConnectionException if connection is unavailable.
        **/
        public static void TeardownProxies()
        {
            try
            {
                if (motionProxy != null) {
                    motionProxy.Dispose();
                    Console.WriteLine("Deleted Motion Proxy");
                }
                if (postureProxy != null)
                {
                    Console.WriteLine("Deleted posture Proxy");
                }
            }
            catch
            {
                throw new Exception("Error deleting Proxies.");
                //throw new UnavailableConnectionException();
            }
        }

        /**
         * Return the IP of the Nao currently connected to.
         **/
        public static string GetIP()
        {
            return ip;
        }

        /**
         * Return the post of the Nao currently connected to.
         **/
        public static int GetPort()
        {
            return port;
        }

        /**
         * Return the location of the Nao currently connected to.
         **/
        public static PointF GetLocation()
        {
            return location;
        }

        /**
         * Return the rotation degree of the Nao currently connected to.
         **/
        public static float GetRotation()
        {
            return rotation;
        }

        public static MotionProxy GetMotionProxy()
        {
            return motionProxy;
        }

        public static RobotPostureProxy GetRobotPostureProxy()
        {
            return postureProxy;
        }

        /**
         * Returns true if connected to a Nao. Otherwise returns false.
         **/
        public static bool IsConnected()
        {
            return connected;
        }

        /**
         * Retrieve the most up to date properties of the Nao currently connected to.
         * These properties include:
         *  - Location
         *  - Rotation
         **/
        public static void Update()
        {
            List<float> vector = motionProxy.getRobotPosition(false);
            location = new PointF(vector[0], vector[1]);
            rotation = vector[2];
        }
    }    
}
