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

        //makes Sonar and memory Proxies
        public Sonar(String ip)
        {
            try
            {
                sonarProxy = new SonarProxy(ip, 9559);
                memoryProxy = new MemoryProxy(ip, 9559);
            }
            catch (InvalidCastException e)
            {
                Console.Error.WriteLine(e);
                Console.Error.WriteLine("JORIK SONARPROXY FAAAALTTT");
            }
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
        public void getSonarDataLeft()
        { 
            Object leftSonar = memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value");
            Object leftSonar1 = memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value1");
            Object leftSonar2 = memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value2");
            Object leftSonar3 = memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value3");
            Object leftSonar4 = memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value4");
            Console.WriteLine("LeftSonar" + leftSonar);
            Console.WriteLine("LeftSonar1" + leftSonar1);
            Console.WriteLine("LeftSonar2" + leftSonar2);
            Console.WriteLine("LeftSonar3" + leftSonar3);
            Console.WriteLine("LeftSonar4" + leftSonar4);
        }

        /*
         * get value of sonar left
         * not done yet
         * */
        public void getSonarDataRight()
        {
            Object rightSonar = memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value");
            Object rightSonar1 = memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value1");
            Object rightSonar2 = memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value2");
            Object rightSonar3 = memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value3");
            Object rightSonar4 = memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value4");
            Console.WriteLine("RightSonar" + rightSonar);
            Console.WriteLine("RightSonar1" + rightSonar1);
            Console.WriteLine("RightSonar2" + rightSonar2);
            Console.WriteLine("RightSonar3" + rightSonar3);
            Console.WriteLine("RightSonar4" + rightSonar4);
        }
    }
}
