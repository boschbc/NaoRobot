using System;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using NUnit.Framework;

using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Test.Event
{
    public class EventTestingUtilities
    {
        /// <summary>
        /// Creates a stream and fill it with data.
        /// </summary>
        /// <param name="input">Data which will be writen into the stream.</param>
        /// <returns>A new communication stream.</returns>
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
        /// Uses reflection to get the field value from an object.
        /// </summary>
        ///
        /// <param name="type">The instance type.</param>
        /// <param name="instance">The instance object.</param>
        /// <param name="fieldName">The field's name which is to be fetched.</param>
        ///
        /// <returns>The field value from the object.</returns>
        public static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }

        /// <summary>
        ///  Connects to Webots client.
        ///  <exception cref="UnavailableConnectionException">Webots is not running.</exception>
        /// </summary>
        public static void ConnectWebots()
        {
            NaoState.Instance.Connect("127.0.0.1", 9559);
        }

        /// <summary>
        /// Requires a valid connection to the Webots client. If no connection is found then a test's result becomes inconclusive.
        /// </summary>
        /// <returns>True if a connection to Webots was established succesfully.</returns>
        public static bool RequireWebots()
        {
            Logger.Log(typeof(EventTestingUtilities),"Requiring Webots");
            try
            {
                EventTestingUtilities.ConnectWebots();
                Logger.Log(typeof(EventTestingUtilities), "Connected Webots");
                return true;
            }
            catch (UnavailableConnectionException)
            {
                Assert.Inconclusive();
                return false;
            }
        }

        [HandleProcessCorruptedStateExceptions]
        public static bool DisconnectWebots()
        {
            if (Naovigate.Util.NaoState.Instance.Connected)
            {
                bool res;
                try
                {
                    Walk.Instance.StopMove();
                    res = true;
                }
                catch{ res = false; }
                return res;
            }
            return true;
        }
    }
}
