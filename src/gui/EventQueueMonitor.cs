using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public partial class EventQueueMonitor : UserControl
    {
        public EventQueueMonitor()
        {
            EventQueue.Nao.SubscribeFire(UpdateContent);
            InitializeComponent();
        }

        public void UpdateContent(INaoEvent e)
        {
            if (eventLabel.InvokeRequired)
            {
                eventLabel.Invoke(new MethodInvoker(() => UpdateContent(e)));
            }
            Logger.Log(this, "Event fire detected.");
            eventLabel.Text = e.ToString();
        }
    }
}
