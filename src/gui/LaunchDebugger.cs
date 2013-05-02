using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            NaoState.ConnectTo(ip, port);
            //Console.WriteLine("hier komt die");
            Application.Run(new NaoDebugger());
        }
    }
}
