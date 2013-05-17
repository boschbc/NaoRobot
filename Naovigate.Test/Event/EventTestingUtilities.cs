using System;
using System.IO;

using Naovigate.Communication;

namespace Naovigate.Test.Event
{
    class EventTestingUtilities
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
    }
}
