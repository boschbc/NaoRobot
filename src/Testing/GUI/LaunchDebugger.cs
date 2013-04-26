using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Util;
using Naovigate.GUI;

namespace Naovigate.Testing.gui
{
    class LaunchDebugger
    {
        public static void DebugMain(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 9559;
            NaoState.ConnectTo(ip, port);
            Application.Run(new NaoDebugger());
        }
    }
}
