using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Naovigate.Event;

namespace Naovigate.GUI
{
    public partial class EventLauncherPanel : UserControl
    {
        public EventLauncherPanel()
        {
            InitializeComponent();
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            MoveNaoEvent moveEvent = new MoveNaoEvent(0.5f, 0.0f);
            //moveEvent.Fire();
            EventQueue.Instance.Post(moveEvent);
           // eventLauncher.RunWorkerAsync();
        }

        private void eventLauncher_DoWork(object sender, DoWorkEventArgs e)
        {
          //  MoveNaoEvent moveEvent = new MoveNaoEvent(0.5f, 0.0f);
          //  moveEvent.Fire();
        }
    }
}
