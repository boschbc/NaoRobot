using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;
using System.Net.Sockets;
using System.Net;
using Naovigate.Util;

namespace Naovigate.Communication
{
    public class IPScanner
    {
        private static readonly String baseIP = "192.168.";
        
        /// <summary>
        /// Finds a IP of a Nao by going through all the last numbers of
        /// a Nao's ip
        /// </summary>
        /// <param name="last"></param>
        /// <returns></returns>
        public static String getNextIP(int last)
        {
            for (int i = last < 0 ? 0 : last; i <= 255; i++)
            {
                if (testIP(baseIP + 0 + "." + i))
                {
                    return baseIP + 0 + "." + i;
                }
                else if (testIP(baseIP + 1 + "." + i))
                {
                    return baseIP + 1 + "." + i;
                }
            }
            return null;
        }

        /// <summary>
        /// Test if the given IP is reachable on port 9559.
        /// </summary>
        /// <param name="ip">the ip</param>
        /// <returns>true if a Nao is listening on this ip</returns>
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
                Logger.Log(typeof(IPScanner), ex.Message);
            }
            return false;
        }
    }
}
