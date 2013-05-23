using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Naovigate.GUI
{
    public partial class GoalSimulator : UserControl
    {
        public GoalSimulator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invokes the GOAL simulator using the arguments provided in the text-box.
        /// </summary>
        /// <param name="sender">Object which called this event</param>
        /// <param name="e">Event arguments</param>
        private void simulateButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Executing command: " + commandBox.Text);
            GoalStub.Execute(commandBox.Text);
        }
    }
}
