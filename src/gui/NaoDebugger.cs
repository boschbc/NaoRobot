using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Naovigate.gui
{
    public partial class NaoDebugger : Form
    {
        private RealtimeField[] DebugWidgets;

        public NaoDebugger(string ip, int port)
        {
            InitializeComponent();
            InitializeMonitors(ip, port);
        }

        /**
         * Update the debug data displayed in all debug fiels.
         **/
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            foreach (RealtimeField rf in DebugWidgets)
            {
                rf.UpdateContent();
            }
        }

        private void NaoDebugger_Load(object sender, EventArgs e)
        {
            //DebugWidgets = new RealtimeField[]
        }
    }
}
