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
        public static Form DebugMain()
        {
            return StartDebugger();
        }

        /// <summary>
        /// Runs the debugger.
        /// </summary>
        private static Form StartDebugger()
        {
            NaoDebugger db = new NaoDebugger();
            Application.EnableVisualStyles();
            Application.Run(db);
            return db;
        }
    }
}
