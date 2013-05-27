using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;
using System.Net.Sockets;
using System.Net;

namespace Naovigate.Communication
{
    public class IPScanner
    {
        /*
         * Finds a IP of a Nao by going through all the last numbers of
         * a Nao's ip
         */
        public static String getNextIP(int last)
        {
            for (int i = last < 0 ? 0 : last; i <= 255; i++)
            {
                if (testIP("192.168.0." + i))
                {
                    return "192.168.0." + i;
                }
                else if (testIP("192.168.1." + i))
                {
                    return "192.168.1." + i;
                }
            }
            return null;
        }

        /*
         * Test if the given IP is reachable on port 9559.
         */
        private static Boolean testIP(String ip)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint endPoint = new IPEndPoint(ipAddress, 9559);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IAsyncResult res = socket.BeginConnect(endPoint, null, null);
                if (res.AsyncWaitHandle.WaitOne(100))
                {
                    TextToSpeechProxy proxy = new TextToSpeechProxy(ip, 9559);
                    proxy.say("Connected");
                    return true;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
                return false;
        }
    }
}
