using System;
using Naovigate.Communication;

namespace Naovigate.Test.Communication
{
    class DefaultCommunicationStreamTest : AbstractCommunicationStreamTest
    {
        public override void Initialize()
        {
            stream = new CommunicationStream(internalStream);
        }
    }
}
