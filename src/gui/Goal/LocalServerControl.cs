using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI.Server
{
    public partial class LocalServerControl : UserControl
    {
        public LocalServerControl()
        {
            InitializeComponent();
        }

        private void TurnOn()
        {
            button.BackColor = Color.LightGreen;
            button.Text = "ON";
            GoalServer.Instance.Start("127.0.0.1", GoalCommunicator.DefaultPort);
        }

        private void TurnOff()
        {
            button.BackColor = Color.LightCoral;
            button.Text = "OFF";
            GoalServer.Instance.Close();
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (button.Text.Equals("OFF"))
                TurnOn();
            else if (button.Text.Equals("ON"))
                TurnOff();
        }
    }
}
