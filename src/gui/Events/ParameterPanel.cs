using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A control that contains parameters for given methods.
    /// </summary>
    public sealed partial class ParameterPanel : UserControl
    {
        private int rowIndex = 0;
        private Dictionary<String, IParamChooser> parameterMap = 
            new Dictionary<String, IParamChooser>();

        public ParameterPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Adds a parameter to the panel.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        /// <param name="chooser">
        /// The parameter's chooser.
        /// A chooser is a control that allows the user to choose the given parameter.
        /// </param>
        public void AddParameter(string name, IParamChooser chooser)
        {
            parameterMap.Add(name, chooser);
            Label label = new Label();
            Control paramControl = (Control) chooser;
            label.Text = name;
            contentTabelPanel.Controls.Add(label, 0, rowIndex);
            contentTabelPanel.Controls.Add(paramControl, 1, rowIndex);
            rowIndex++;
        }

        /// <summary>
        /// Clears the panel of all parameters.
        /// </summary>
        public void ClearAllParameters()
        {
            contentTabelPanel.Controls.Clear();
            rowIndex = 0;
            parameterMap.Clear();
        }

        /// <summary>
        /// Returns the value of one of the parameters present in the panel.
        /// </summary>
        /// <param name="name">The name of the parameter whose value to get.</param>
        /// <returns>The given parameter's value.</returns>
        public Object GetParameterValue(string name)
        {
            return parameterMap[name].Value;
        }
    }
}
