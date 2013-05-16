using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Naovigate.GUI
{
    public partial class NaoDebugger : Form
    {
        public NaoDebugger()
        {
            InitializeComponent();
        }

        private void NaoDebugger_FormClosed(object sender, FormClosedEventArgs e)
        {
            stateMonitorPanel.StopUpdate();
            cameraMonitor.StopUpdate();
        }
    }
}
