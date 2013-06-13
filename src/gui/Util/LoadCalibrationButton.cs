using System;
using System.Windows.Forms;
using Naovigate.Util;

namespace Naovigate.GUI.Util
{
    public partial class LoadCalibrationButton : UserControl
    {
        public LoadCalibrationButton()
        {
            InitializeComponent();
        }

        private string[] List()
        {
            //TODO add to dropdown
            return System.IO.Directory.GetFiles("../resources/calibs/", "*.naocalib");
        }

        private void dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = (string) dropdown.SelectedItem;
            string path = "../resources/calibs/" + filename + ".naocalib";
            Calibration.Instance = new Calibration(path);
        }
    }
}
