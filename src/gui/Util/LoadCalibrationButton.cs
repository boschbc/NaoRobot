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
            FillDropdown();
        }

        private void FillDropdown()
        { 
            string[] files = System.IO.Directory.GetFiles("../resources/calibs/", "*.naocalib");
            foreach (string name in files)
                dropdown.Items.Add(System.IO.Path.GetFileNameWithoutExtension(name));
        }

        private void dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = (string) dropdown.SelectedItem;
            string path = "../resources/calibs/" + filename + ".naocalib";
            Calibration.Instance = new Calibration(path);
        }
    }
}
