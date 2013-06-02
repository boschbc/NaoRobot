using System;
using Naovigate.Communication;
using NUnit.Framework;
namespace Naovigate.Test.Communication
{
    [TestFixture, Timeout(1000)]
    class DefaultCommunicationStreamTest : AbstractCommunicationStreamTest
    {
        public override void Initialize()
        {
            stream = new CommunicationStream(internalStream);
        }
    }
}
