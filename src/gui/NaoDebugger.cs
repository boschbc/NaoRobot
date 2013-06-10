using System;
using System.Windows.Forms;

namespace Naovigate.GUI
{
    /// <summary>
    /// A debugger aimed at helping developers ineteracting with Nao robots and Goal servers using Naovigate and
    /// a graphical user interface.
    /// </summary>
    public sealed partial class NaoDebugger : Form
    {
        public NaoDebugger()
        {
            InitializeComponent();
            tabControl.SelectedIndex = 2;
            Load += new EventHandler(NaoDebugger_Load);
            FormClosing += new FormClosingEventHandler(NaoDebugger_FormClosing);
        }

        /// <summary>
        /// Activates the state-monitor on load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NaoDebugger_Load(object sender, EventArgs e)
        {
           stateMonitorPanel.Active = true;
        }

        /// <summary>
        /// Deactivates the state-monitor and the live-camera controls when closing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NaoDebugger_FormClosing(object sender, FormClosingEventArgs e)
        {
            stateMonitorPanel.Active = false;
            liveCamera.Active = false;
        }
    }
}
