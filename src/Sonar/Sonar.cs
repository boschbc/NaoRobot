using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;

namespace Naovigate.Sonar
{
    class Sonar
    {
        private SonarProxy sonarProxy;
        private MemoryProxy memoryProxy;

        private static Sonar instance = null;

        //makes Sonar and memory Proxies
        public Sonar(String ip)
        {
                sonarProxy = new SonarProxy(ip, 9559);
                memoryProxy = new MemoryProxy(ip, 9559);
        }

        public static Sonar GetInstance()
        {
            return instance == null ? new Sonar(Util.NaoState.GetIP()) : instance;
        }

        //activates sonar
        public void activateSonar()
        {
            try
            {
                sonarProxy.subscribe("Nao");
                Console.WriteLine("subscribed");
            }
            catch (Exception)
            {
                Console.WriteLine("No sonar subscription");
            }                            
        }
        //deavtivates sonar
        public void deactivateSonar()
        {
            sonarProxy.unsubscribe("Nao");
            Console.WriteLine("Deactivating sonar");
        }
        /*
         * get value of sonar left
         * not done yet
         * */
        public float getSonarDataLeft()
        { 
            return (float)memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value");            
        }

        /*
         * get value of sonar left
         * not done yet
         * */
        public float getSonarDataRight()
        {
            return (float)memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value");
        }
    }
}
