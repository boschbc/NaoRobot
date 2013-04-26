﻿using System;
using System.Net;
using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate
{
    class MainProgram
    {
        public static void Main(String[] args)
        {
            //NaoProxyManager.Instance.EndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9550);
            //Testing.GoalCommunicatorTest1 goalCom = new Testing.GoalCommunicatorTest1(args);
            //hoi hoi
            Sonar.Sonar sn = new Sonar.Sonar("192.168.0.126");
            sn.deactivateSonar();
            sn.activateSonar();
            while (true)
            {
                sn.getSonarDataLeft();
                sn.getSonarDataRight();
                Console.WriteLine();
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
