using System;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.GUI;
using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public static class LaunchDebugger
    {
        public static void DebugMain()
        {
            StartDebugger();
        }

        private static void StartDebugger()
        {
            Application.EnableVisualStyles();
            Application.Run(new NaoDebugger());
        }
    }
}
