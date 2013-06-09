using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Naovigate.GUI.Util
{
    public sealed partial class PointControl : UserControl
    {
        public PointControl()
        {
            InitializeComponent();
        }

        public int X
        {
            get { return (int) x.Value; }
        }

        public int Y
        {
            get { return (int) y.Value; }
        }
    }
}
