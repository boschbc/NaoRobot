using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.GUI.Events;
namespace Naovigate.GUI
{
    public sealed partial class NaoDebugger : Form
    {
        public NaoDebugger()
        {
            InitializeComponent();
            tabControl.SelectedIndex = 2;
            Load += new EventHandler(NaoDebugger_Load);
            FormClosing += new FormClosingEventHandler(NaoDebugger_FormClosing);
        }

        void NaoDebugger_Load(object sender, EventArgs e)
        {
           stateMonitorPanel.Active = true;
        }

        void NaoDebugger_FormClosing(object sender, FormClosingEventArgs e)
        {
            stateMonitorPanel.Active = false;
            liveCamera.Active = false;
        }
    }
}
