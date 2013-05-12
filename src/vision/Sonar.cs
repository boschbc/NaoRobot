using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;
using System.Timers;
using Naovigate.Event;

namespace Naovigate.Vision
{
    public class Sonar
    {
        private SonarProxy sonarProxy;
        private MemoryProxy memoryProxy;
        private Timer timer;

        private static Sonar instance = null;

        /*
         * makes Sonar and memory Proxies
         */
        public Sonar(String ip)
        {
                sonarProxy = new SonarProxy(ip, 9559);
                memoryProxy = new MemoryProxy(ip, 9559);

                ActivateSonar();

                timer = new Timer();
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Interval = 500;
        }

        public static Sonar Instance
        {
            get{
                return instance == null ? instance = new Sonar(Util.NaoState.IP.ToString()) : instance;
            }
        }

        /*
         * activates sonar
         */
        public void ActivateSonar()
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

        /*
         * deactivates sonar
         */
        public void Deactivate()
        {
            StopChecking();
            sonarProxy.unsubscribe("Nao");
            Console.WriteLine("Deactivating sonar");
        }

        /*
         * Check if the Nao is too close (within 0.3 metres) to a wall (or other object)
         * If the Nao is too close, raise a NaoCollidingEvent and stop checking
         * */
        public void OnTimedEvent(object source, ElapsedEventArgs ev) {
            if (timer.Enabled)
            {
                float left = getSonarDataLeft();
                float right = getSonarDataRight();

                bool collidingLeft = left <= 0.3;
                bool collidingRight = right <= 0.3;

                if (collidingLeft || collidingRight)
                {
                    EventQueue.Instance.Enqueue(new NaoCollidingEvent(collidingLeft, collidingRight));
                    StopChecking();
                }
            }
        }

        /*
         * Start checking for collisions
         * */
        public void StartChecking()
        {
            timer.Start();
        }

        /*
         * Stop checking for collisions
         * */
        public void StopChecking()
        {
            timer.Stop();
        }

        /*
         * get value of sonar left
         * */
        public float getSonarDataLeft()
        { 
            return (float)memoryProxy.getData("Device/SubDeviceList/US/Left/Sensor/Value");            
        }

        /*
         * get value of sonar left
         * */
        public float getSonarDataRight()
        {
            return (float)memoryProxy.getData("Device/SubDeviceList/US/Right/Sensor/Value");
        }
    }
}
