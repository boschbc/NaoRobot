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

namespace Naovigate.GUI.Goal
{
    public partial class RemoteServerControl : UserControl
    {
        private GoalCommunicator communicator;

        public RemoteServerControl()
        {
            InitializeComponent();
            ipChooser.IP = MainProgram.GoalIP;
        }

        private void Connect()
        {
            if (communicator.Connect())
                communicator.StartAsync();
        }

        private void button_Click(object sender, EventArgs e)
        {   
            if (communicator != null)
                communicator.Close();
            communicator = new GoalCommunicator(ipChooser.IP, GoalCommunicator.DefaultPort);
            GoalCommunicator.Instance = communicator;
            new Thread(new ThreadStart(Connect)).Start();
        }
    }
}
