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
    public sealed partial class IPChooser : UserControl
    {
        public IPChooser()
        {
            InitializeComponent();
            ip1.GotFocus += new EventHandler(field_GotFocus);
            ip2.GotFocus += new EventHandler(field_GotFocus);
            ip3.GotFocus += new EventHandler(field_GotFocus);
            ip4.GotFocus += new EventHandler(field_GotFocus);
        }

        private void SetField(NumericUpDown field, int value)
        {
            if (field.InvokeRequired)
                field.Invoke(new MethodInvoker(() => SetField(field, value)));
            else
                field.Value = value;
        }

        public string IP
        {
            get
            {
                return String.Format("{0}.{1}.{2}.{3}",
                    ip1.Value, ip2.Value, ip3.Value, ip4.Value);
            }
            set
            {
                string[] values = value.Split('.');
                NumericUpDown[] fields = new NumericUpDown[4] { ip1, ip2, ip3, ip4 };
                for (int i = 0; i < fields.Length; i++)
                    SetField(fields[i], Int32.Parse(values[i]));
            }
        }

        void field_GotFocus(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, 3);
        }
    }
}
