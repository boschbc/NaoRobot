using System.Windows.Forms;

namespace Naovigate.GUI.Util
{
    /// <summary>
    /// A control that allows the user to specify a point's x, y coordinates.
    /// </summary>
    public sealed partial class PointControl : UserControl
    {
        public PointControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// X-coordinate.
        /// </summary>
        public int X
        {
            get { return (int) x.Value; }
        }

        /// <summary>
        /// Y-coordinate.
        /// </summary>
        public int Y
        {
            get { return (int) y.Value; }
        }
    }
}
