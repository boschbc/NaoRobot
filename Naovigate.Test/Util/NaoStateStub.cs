using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

using Aldebaran.Proxies;
using Naovigate.Util;
using Naovigate.Movement;
using Naovigate.Communication;

namespace Naovigate.Test.Util
{
    public class NaoStateStub : NaoState
    {
        public NaoStateStub()
        {
            NaoState.Instance = this;
        }

        public override void Connect(string ip, int port)
        {
            connected = true;
        }

        public override void Connect(IPEndPoint endPoint)
        {
            connected = true;
        }

        public override void Disconnect()
        {
            connected = false;
        }

        protected override TProxy createProxy<TProxy>(Func<string, int, TProxy> proxy)
        {
            return default(TProxy);
        }
    }
}
