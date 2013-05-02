using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.Testing
{
    class SonarTest
    {
        public SonarTest(String ip)
        {
            Sonar.Sonar sn = new Sonar.Sonar(ip);
            sn.activateSonar();
            sn.getSonarDataLeft();
            sn.getSonarDataRight();
            sn.deactivateSonar();
            System.Threading.Thread.Sleep(1000);
        }
    }
}
