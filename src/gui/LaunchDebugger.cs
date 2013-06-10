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
    /// <summary>
    /// A wrapper class that launches the NaoDebugger.
    /// </summary>
    public static class LaunchDebugger
    {
        /// <summary>
        /// Starts the debugger.
        /// </summary>
        public static void DebugMain()
        {
            StartDebugger();
        }

        /// <summary>
        /// Runs the debugger.
        /// </summary>
        private static void StartDebugger()
        {
            Application.EnableVisualStyles();
            Application.Run(new NaoDebugger());
        }
    }
}
