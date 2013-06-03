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

namespace Naovigate.GUI.State
{
    public partial class NaoConnection : UserControl
    {
        public NaoConnection()
        {
            InitializeComponent();
            ipChooser.IP = MainProgram.NaoIP;
        }

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
