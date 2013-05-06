using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;
using Naovigate.Util.NaoState;

namespace Naovigate.Vision
{
    public class Sonar
    {
        private static string leftDataLocation = "Device/SubDeviceList/US/Left/Sensor/Value";
        private static string rightDataLocation = "Device/SubDeviceList/US/Right/Sensor/Value";
        private SonarProxy sonarProxy;
        private MemoryProxy memoryProxy;
        private static Sonar instance = null;

        public Sonar()
        {
            this.sonarProxy = NaoState.SonarProxy;
            this.memoryProxy = NaoState.MemoryProxy;
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        /// <value>The instance.</value>
        public static Sonar Instance
        {
            get
            {
                if (instance == null) {
                    instance = new Sonar();
                }
                return instance;
            }
        }

        /// <summary>
        /// Activate sonar.
        /// </summary>
        public void Activate()
        {
            this.sonarProxy.subscribe("Nao");                        
        }

        /// <summary>
        /// Deactivate the sonar.
        /// </summary>
        public void Deactivate()
        {
            this.sonarProxy.unsubscribe("Nao");
        }

        /// <summary>
        /// The detected range to nearest wall from the left sonar.
        /// </summary>
        /// <value>The left data.</value>
        public float LeftData {
            get {
                return (float)this.memoryProxy.getData(leftDataLocation);
            }
        }

        /// <summary>
        /// The detected range to nearest wall from the right sonar.
        /// </summary>
        /// <value>The right data.</value>
        public float RightData {
            get {
                return (float)this.memoryProxy.getData(rightDataLocation);
            }
        }
    }
}
