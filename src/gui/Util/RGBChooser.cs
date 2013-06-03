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
    public partial class RGBChooser : UserControl
    {
        public RGBChooser()
        {
            InitializeComponent();
            HookFocusEvent();
        }

        private void HookFocusEvent()
        {
            red.GotFocus += new EventHandler(Channel_GotFocus);
            green.GotFocus += new EventHandler(Channel_GotFocus);
            blue.GotFocus += new EventHandler(Channel_GotFocus);
        }

        private void Channel_GotFocus(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, 3);
        }

        public double[] RGB
        {
            get 
            {
                if (red.InvokeRequired)
                    return (double[]) red.Invoke(new Func<double[]>(() => RGB));
                else if (green.InvokeRequired)
                    return (double[])green.Invoke(new Func<double[]>(() => RGB));
                else if (blue.InvokeRequired)
                    return (double[])blue.Invoke(new Func<double[]>(() => RGB));
                else return new double[3]
                { 
                    (double) red.Value, 
                    (double) green.Value, 
                    (double) blue.Value 
                }; 
            }
        }
    }
}
