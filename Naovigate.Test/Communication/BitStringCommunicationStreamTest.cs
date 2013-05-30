using System;
using Naovigate.Communication;

namespace Naovigate.Test.Communication
{
    class BitStringCommunicationStreamTest : AbstractCommunicationStreamTest
    {
        public override void Initialize()
        {
            stream = new BitStringCommunicationStream(internalStream);
        }
    }
}
