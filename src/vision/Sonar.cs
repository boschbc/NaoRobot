using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;
using Naovigate.Util;
using Naovigate.Event;

namespace Naovigate.Vision
{
    public sealed class Sonar
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
        public void ActivateSonar()
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
        /// deactivates sonar
        /// </summary>
        public void Deactivate()
        {
            sonarProxy.unsubscribe("Nao");
            Logger.Log(this, "Deactivating sonar");
        }

        /// <summary>
        /// Check if the Nao is too close (within 0.3 metres) to a wall (or other object)
        /// </summary>
        /// <returns></returns>
        public bool IsTooClose() {
                float left = getSonarDataLeft();
                float right = getSonarDataRight();

                bool collidingLeft = left <= 0.3;
                bool collidingRight = right <= 0.3;

                return (collidingLeft || collidingRight);
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
    }
}
