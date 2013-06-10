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
    /// A simple numeric up-down allowing the choice of integers and flotas (up to 2 decimal places).
    /// </summary>
    public sealed partial class IntegerChooser : UserControl, IParamChooser
    {
        public IntegerChooser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The value of this chooser.
        /// </summary>
        public Object Value
        {
            get { return value.Value; }
        }
    }
}
