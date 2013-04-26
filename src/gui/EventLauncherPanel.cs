using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Communication;

namespace Naovigate.GUI
{
    public partial class EventLauncherPanel : UserControl
    {
        private Thread moveThread;

        public EventLauncherPanel()
        {
            InitializeComponent();
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            MoveNaoEvent moveEvent = new MoveNaoEvent(0.5f, 0.0f);
            moveThread = new Thread(new ThreadStart(moveEvent.Fire));
            moveThread.Start();
        }
    }
}
