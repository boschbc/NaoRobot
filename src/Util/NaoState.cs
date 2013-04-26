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

        public static void ConnectTo(string ip_, int port_)
        {
            ip = ip_;
            port = port_;
            CreateProxies();
            Update();
        }

        private static void CreateProxies()
        {
            motionProxy = new MotionProxy(GetIP(), GetPort());
            postureProxy = new RobotPostureProxy(GetIP(), GetPort());
            Walk.RefreshProxies();
        }

        public static string GetIP()
        {
            return ip;
        }

        public static int GetPort()
        {
            return port;
        }

        public static PointF GetLocation()
        {
            return location;
        }

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

        public static void Update()
        {
            List<float> vector = motionProxy.getRobotPosition(false);
            location = new PointF(vector[0], vector[1]);
            rotation = vector[2];
        }
    }    
}
