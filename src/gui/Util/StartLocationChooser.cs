using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI.Util
{
    public partial class StartLocationChooser : UserControl
    {
        public StartLocationChooser()
        {
            InitializeComponent();
        }

        private void startLocation_ValueChanged(object sender, EventArgs e)
        {
            Logger.Log(this, "Starting location set: " + startLocation.Value); 
            GoalCommunicator.initialPosition = (int) startLocation.Value;
        }
    }
}
