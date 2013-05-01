using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Aldebaran.Proxies;

namespace Nao
{
	public class Program
	{
        public static String ip = "192.168.0.116";
		public static void AMain()
		{
            //Walk();
            //OtherTest();
            Say("Using System 1337");
            //WalkTime(5000);
            //Stop();
		}

        public static void Say(String say)
        {
            TextToSpeechProxy tts = new TextToSpeechProxy(ip, 9559);
            tts.say(say);
        }

        public static void Walk()
        {
            TextToSpeechProxy tts = new TextToSpeechProxy(ip, 9559);
            tts.say("Hello");
            MotionProxy motionProxy = new MotionProxy(ip, 9559);
            RobotPostureProxy posture = new RobotPostureProxy(ip, 9559);
            posture.goToPosture("StandZero", 1F);
            motionProxy.moveInit();
            motionProxy.move(10, 0, 0.3F);
            Thread.Sleep(10000);
            motionProxy.stopMove();
            //posture.goToPosture("SitRelax", 1F);
        }

        public static void OtherTest()
        {
            RobotPostureProxy posture = new RobotPostureProxy(ip, 9559);
            posture.goToPosture("StandInit", 0.7F);
            //posture.goToPosture("StandZero", 0.1F);
        }

        public static void WalkTime(int time)
        {
            MotionProxy motionProxy = new MotionProxy(ip, 9559);
            motionProxy.moveInit();
            motionProxy.move(1f, 0, 0.3F);
            Thread.Sleep(time);
            motionProxy.stopMove();
        }

        public static void Stop()
        {
            MotionProxy motionProxy = new MotionProxy(ip, 9559);
            motionProxy.stopMove();
        }
	}
}

