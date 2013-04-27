using System;
using System.Drawing;
using System.Collections.Generic;

using Aldebaran.Proxies;

using Naovigate.Movement;
using Naovigate.Communication;

namespace Naovigate.Util
{
    public static class NaoState
    {
        private static string ConnectionErrorMsg = "Connection Unavailable.";
        private static string ProxyDeletionErrorMsg = "Error while disposing of proxies.";
        private static string UpdateErrorMsg = "Failed update - no connection.";

        private static string ip;
        private static int port;
        private static PointF location;
        private static float rotation;

        private static MotionProxy motionProxy;
        private static RobotPostureProxy postureProxy;

        private static bool connected = false;

        /**
         * Connect to a Nao.
         * If already connected to a Nao, will disonnect first.
         * @param ip_ = IP address of target Nao.
         * @param port_ = Integer of the desired port.
         **/
        public static void ConnectTo(string ip_, int port_)
        {
            if (IsConnected())
                Disconnect();

            ip = ip_;
            port = port_;
            CreateProxies();
            connected = true;
            Update();
        }

        /**
         * Disconnects from the Nao currently connected to.
         * If no Nao is connected, does nothing.
         **/
        public static void Disconnect()
        {
            ip = null;
            port = -1;
            TeardownProxies();
            connected = false;
        }

        /**
         * Create proxies for the Nao this class is connected to.
         * @throws UnavailableConnectionException if connection is unavailable.
         **/
        private static void CreateProxies()
        {
            try
            {
                motionProxy = new MotionProxy(ip, port);
                postureProxy = new RobotPostureProxy(ip, port);
            }
            catch
            {
                throw new UnavailableConnectionException(ConnectionErrorMsg, ip, port);
            }
        }

        /**
        * Delete proxies for the Nao this class is connected to.
        * @throws UnavailableConnectionException if connection is unavailable.
        **/
        private static void TeardownProxies()
        {
            if (!IsConnected())
                return;
            try
            {
                motionProxy.Dispose();
                Console.WriteLine("Deleted Motion Proxy");
                postureProxy.Dispose();
                Console.WriteLine("Deleted Posture Proxy");
            }
            catch
            {
                throw new UnavailableConnectionException(ProxyDeletionErrorMsg, ip, port);
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
         *  @throws UnavailableConnectionException if not connected to a Nao.
         **/
        public static void Update()
        {
            Console.WriteLine("Updating.");

            if (!IsConnected())
                throw new UnavailableConnectionException(UpdateErrorMsg, ip, port);
            try
            {
                List<float> vector = motionProxy.getRobotPosition(false);
                location = new PointF(vector[0], vector[1]);
                rotation = vector[2];
            }
            catch
            {
                Console.WriteLine("Faild Nao update.");
            }
            
        }
    }    
}
