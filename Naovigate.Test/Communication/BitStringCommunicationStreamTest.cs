using System;
using Naovigate.Communication;
using NUnit.Framework;

namespace Naovigate.Test.Communication
{
    [TestFixture, Timeout(1000)]
    class BitStringCommunicationStreamTest : AbstractCommunicationStreamTest
    {
        public override void Initialize()
        {
            stream = new BitStringCommunicationStream(internalStream);
        }
    }
}
