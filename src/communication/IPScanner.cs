using System;
using System.Net;
using System.Net.Sockets;
using Aldebaran.Proxies;
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
        /// <returns>An ip address of a nao, or null if no Nao was found.</returns>
        public static String GetNextIP(int last)
        {
            for (int i = last < 0 ? 0 : last; i <= 255; i++)
            {
                if (TestIP(baseIP + 0 + "." + i))
                {
                    return baseIP + 0 + "." + i;
                }
                else if (TestIP(baseIP + 1 + "." + i))
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
        private static bool TestIP(String ip)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint endPoint = new IPEndPoint(ipAddress, 9559);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IAsyncResult res = socket.BeginConnect(endPoint, null, null);
                if (res.AsyncWaitHandle.WaitOne(100))
                    return IsNao(ip);
            }
            catch (Exception ex) {
                Logger.Log(typeof(IPScanner), ex.Message);
            }
            return false;
        }

        private static bool IsNao(string ip)
        {
            TextToSpeechProxy proxy = new TextToSpeechProxy(ip, 9559);
            proxy.say("Connected");
            return true;
        }
    }
}
