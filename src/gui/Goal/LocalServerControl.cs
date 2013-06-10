using System;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Communication;

namespace Naovigate.GUI.Goal
{
    /// <summary>
    /// A control that allows the user to turn the local goal server on or off.
    /// </summary>
    public sealed partial class LocalServerControl : UserControl
    {
        public LocalServerControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Turns the local goal server on.
        /// </summary>
        private void TurnOn()
        {
            button.BackColor = Color.LightGreen;
            button.Text = "ON";
            GoalServer.Instance.Start("127.0.0.1", GoalCommunicator.DefaultPort);
        }

        /// <summary>
        /// Turns the local goal server off.
        /// </summary>
        private void TurnOff()
        {
            button.BackColor = Color.LightCoral;
            button.Text = "OFF";
            GoalServer.Instance.Close();
        }

        /// <summary>
        /// Toggles the state of the local goal server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            if (button.Text.Equals("OFF"))
                TurnOn();
            else if (button.Text.Equals("ON"))
                TurnOff();
        }
    }
}
