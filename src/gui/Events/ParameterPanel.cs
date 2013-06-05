using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Naovigate.GUI.Events
{
    public partial class ParameterPanel : UserControl
    {
        private int rowIndex = 0;
        private Dictionary<String, IParamChooser> parameterMap = 
            new Dictionary<String, IParamChooser>();

        public ParameterPanel()
        {
            InitializeComponent();
        }

        public void AddParameter(string name, IParamChooser chooser)
        {
            parameterMap.Add(name, chooser);
            Label label = new Label();
            Control paramControl = (Control) chooser;
            label.Text = name;
            contentTabelPanel.Controls.Add(label, 0, rowIndex);
            contentTabelPanel.Controls.Add(paramControl, 1, rowIndex);
            rowIndex++;
            doneButton.Visible = true;
        }

        public void ClearAllParameters()
        {
            contentTabelPanel.Controls.Clear();
            doneButton.Visible = false;
            rowIndex = 0;
            parameterMap.Clear();
        }

        public Object GetParameterValue(string name)
        {
            return parameterMap[name].Value;
        }
    }
}
