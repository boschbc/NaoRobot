using System;
using System.Threading;
using System.Windows.Forms;

using Naovigate.Communication;

namespace Naovigate.GUI.Goal
{
    /// <summary>
    /// A class that allows the user to connect to a remote goal server.
    /// </summary>
    public sealed partial class RemoteServerControl : UserControl
    {
        private GoalCommunicator communicator;

        /// <summary>
        /// Creates a new instance of this control and sets the default IP
        /// to the one specified in a constant field in MainProgram.
        /// </summary>
        public RemoteServerControl()
        {
            InitializeComponent();
            ipChooser.IP = MainProgram.GoalIP;
        }

        /// <summary>
        /// Connects the Goal-Communicator to the given IP asynchronously.
        /// </summary>
        private void Connect()
        {
            if (communicator.Connect())
                communicator.StartAsync();
        }

        /// <summary>
        /// Closes the goal communicator if it is already connected and 
        /// attempts to connect to the currently selected IP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
