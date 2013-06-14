using System;
using System.Windows.Forms;

namespace Naovigate.GUI.Util
{
    /// <summary>
    /// A control that allows the user to specify an color in RGB color-space.
    /// </summary>
    public sealed partial class RGBChooser : UserControl
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

        /// <summary>
        /// Select the numeric-text-box control once it gets focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_GotFocus(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, 3);
        }

        /// <summary>
        /// The currently selected RGB value.
        /// </summary>
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
