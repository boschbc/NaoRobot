using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Naovigate.Movement;

namespace Naovigate.GUI
{
    public partial class DemoLauncherPanel : UserControl
    {
        public DemoLauncherPanel()
        {
            InitializeComponent();
        }

        private void launchSonarDemo_Click(object sender, EventArgs e)
        {
            Walk.Instance.WalkTowards(0.0f, 143);
        }
    }
}
