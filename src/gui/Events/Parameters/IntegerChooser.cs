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
    public partial class IntegerChooser : UserControl, IParamChooser
    {
        public IntegerChooser()
        {
            InitializeComponent();
        }

        public Object Value
        {
            get { return value.Value; }
        }
    }
}
