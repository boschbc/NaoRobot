using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Naovigate.GUI.Popups.ParamChooser;

namespace Naovigate.GUI.Popups
{
    public partial class UserInputPopup : Form
    {
        private IParamChooser userChoice;

        public UserInputPopup()
        {
            InitializeComponent();
        }

        public void SetParamChooser(IParamChooser paramChooser)
        {
            userChoice = paramChooser;
            contentPanel.Controls.Clear();
            contentPanel.Controls.Add((Control)userChoice);
            ((Control)userChoice).Dock = DockStyle.Fill;
            ((Control)userChoice).Anchor = (AnchorStyles.Bottom | AnchorStyles.Top |
                                            AnchorStyles.Left | AnchorStyles.Right);

        }
        
        public string Title
        {
            get { return Text; }
            set { Text = value; }
        }

        public Object UserInput
        {
            get { return userChoice.Value; }
        }
    }
}
