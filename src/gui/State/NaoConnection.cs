using System;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI.State
{
    /// <summary>
    /// A control that allows the user to connect to a Nao.
    /// </summary>
    public sealed partial class NaoConnection : UserControl
    {
        public NaoConnection()
        {
            InitializeComponent();
            ipChooser.IP = MainProgram.NaoIP;
        }

        /// <summary>
        /// Connects the to the selected Nao IP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            NaoState.Instance.Disconnect();
            try
            {
                NaoState.Instance.Connect(ipChooser.IP, MainProgram.NaoPort);
                
            }
            catch (UnavailableConnectionException)
            {
                Logger.Log(this,
                    String.Format("Could not connect to Nao at: {0}:{1}",
                    ipChooser.IP, MainProgram.NaoPort));
            }
        }
    }
}
