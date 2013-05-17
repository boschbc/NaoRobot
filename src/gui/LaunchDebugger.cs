using System;
using System.Windows.Forms;

using Naovigate.Util;
using Naovigate.GUI;

namespace Naovigate.Testing.GUI
{
    class LaunchDebugger
    {
        public static void DebugMain(params String[] args)
        {
            string ip= "127.0.0.1";
            if (args.Length > 0)
                ip = args[0];
            
            int port = 9559;
            NaoState.Connect(ip, port);
            Application.Run(new NaoDebugger());
        }
    }
}
