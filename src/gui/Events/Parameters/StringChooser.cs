using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Naovigate.GUI.Events.Parameters
{
    /// <summary>
    /// A simple class allowing the user to input some text into a control.
    /// </summary>
    public sealed partial class StringChooser : UserControl, IParamChooser
    {
        public StringChooser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This chooser's value.
        /// </summary>
        public Object Value
        {
            get { return value.Text; }
        }
    }
}
