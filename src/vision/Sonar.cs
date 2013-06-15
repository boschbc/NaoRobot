using System;
using Aldebaran.Proxies;
using Naovigate.Util;

namespace Naovigate.Vision
{
    internal sealed class Sonar : IDisposable
    {
        private SonarProxy sonarProxy;
        private MemoryProxy memoryProxy;

        private static Sonar instance = null;


        /// <summary>
        /// makes Sonar and memory Proxies
        /// </summary>
        /// <param name="ip"></param>
        public Sonar(String ip)
        {
                sonarProxy = new SonarProxy(ip, 9559);
                memoryProxy = new MemoryProxy(ip, 9559);

                ActivateSonar();
        }

        /// <summary>
        /// The Sonar instance
        /// </summary>
        public static Sonar Instance
        {
            get {
                return instance == null ? instance = new Sonar(Util.NaoState.Instance.IP.ToString()) : instance;
            }
        }

        /// <summary>
        /// activates sonar
        /// </summary>
        private void ActivateSonar()
        {
            try
            {
                sonarProxy.subscribe("Nao");
                Console.WriteLine("subscribed");
            }
            catch (Exception)
            {
                Logger.Log(this, "No sonar subscription");
            }                            
        }
        
        /// <summary>
        /// Check if the Nao is too close (within 0.3 metres) to a wall (or other object)
        /// </summary>
        /// <returns></returns>
        public bool IsTooClose() {
            float left = getSonarDataLeft();
            float right = getSonarDataRight();
            Logger.Log(this, "Left = "+left.Readable()+", Right = "+right.Readable());
            bool collidingLeft = left <= 0.3f && left > 0;
            bool collidingRight = right <= 0.3f && right > 0;
            bool res = (collidingLeft && collidingRight);
            if(res)
                Logger.Log(this, "IsTooClose: "+res);
            if (!res && (collidingLeft || collidingRight))
                Logger.Say("Side Wall");
            return res;
        }
        
        /// <summary>
        /// get value of sonar left
        /// </summary>
        /// <returns></returns>
        public float getSonarDataLeft()
        { 
            return (float)memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value");            
        }

        /// <summary>
        /// get value of sonar left
        /// </summary>
        /// <returns></returns>
        public float getSonarDataRight()
        {
            return (float)memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value");
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            if (sonarProxy != null)
                sonarProxy.Dispose();
            if (memoryProxy != null)
                memoryProxy.Dispose();
        }
    }
}
