using System;
using System.Net;
using System.Collections.Generic;
using Aldebaran.Proxies;

namespace Naovigate.Util
{
    public class NaoProxyManager
    {
        /*
        private Dictionary<string, IDisposable> proxies;
        protected static NaoProxyManager instance = null;
        public IPEndPoint EndPoint;

        public NaoProxyManager()
        {
            this.proxies = new Dictionary<string, IDisposable>();
        }

        //?
        public ProxyType GetProxy<ProxyType>(string name) where ProxyType : IDisposable
        {
            if (!this.proxies.ContainsKey(name)) {
                this.proxies[name] = Activator.CreateInstance(ProxyType, this.EndPoint.Address.ToString(), this.EndPoint.Port);
            }
            return this.proxies[name];
        }

         
        public static NaoProxyManager Instance
        {
            get {
                if (instance == null) {
                    instance = new NaoProxyManager();
                }
                return instance;
            }
        }

        public MotionProxy Motion
        {
            get {
                return this.GetProxy<MotionProxy>("Motion");
            }
        }

        public RobotPostureProxy RobotPosture
        {
            get {
                return this.GetProxy<RobotPostureProxy>("RobotPosture");
            }
        }*/
    }
}

