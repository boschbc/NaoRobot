using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.Test.Event
{
    public class EventTestingUtilities
    {
        /// <summary>
        /// Creates a stream and fill it with data.
        /// </summary>
        /// <param name="input">Data which will be writen into the stream.</param>
        /// <returns></returns>
        public static CommunicationStream BuildStream(params int[] input)
        {
            MemoryStream mem = new MemoryStream();
            CommunicationStream com = new CommunicationStream(mem);

            foreach (int i in input)
            {
                com.WriteInt(i);
            }
            mem.Position = 0;  //Bring the seeker back to the start.
            return com;
        }

        /// <summary>
        ///  Connects to Webots client.
        ///  @throws UnavailableConnectionException if Webots is not running.
        /// </summary>
        public static void ConnectWebots()
        {
            NaoState.Instance.Connect("127.0.0.1", 9559);
        }

        /// <summary>
        /// Requires a valid connection to the Webots client. If no connection is found then a test's result becomes inconclusive.
        /// </summary>
        public static void RequireWebots()
        {
            try
            {
                EventTestingUtilities.ConnectWebots();
            }
            catch (UnavailableConnectionException)
            {
                Assert.Inconclusive();
            }
        }
    }
}
