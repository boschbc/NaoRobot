using System;
using System.IO;
using System.Reflection;
using Naovigate.Communication;

namespace Naovigate.Test.Event
{
    public class EventTestingUtilities
    {
        /*
         * Creates a stream and fill it with data.
         */
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
        /// Uses reflection to get the field value from an object. Credit goes to user "dcp" on StackOverflow.
        /// </summary>
        ///
        /// <param name="type">The instance type.</param>
        /// <param name="instance">The instance object.</param>
        /// <param name="fieldName">The field's name which is to be fetched.</param>
        ///
        /// <returns>The field value from the object.</returns>
        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}
