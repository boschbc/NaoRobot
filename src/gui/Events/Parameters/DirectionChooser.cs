using System;
using System.Windows.Forms;
using Naovigate.Navigation;
namespace Naovigate.GUI.Events.Parameters
{
    public partial class DirectionChooser : UserControl, IParamChooser
    {
        public DirectionChooser()
        {
            InitializeComponent();
        }

        public Object Value
        {
            get
            {
                if (value.SelectedText == "UP")
                    return Direction.Up;
                else if (value.SelectedText == "DOWN")
                    return Direction.Down;
                else if (value.SelectedText == "LEFT")
                    return Direction.Left;
                else if (value.SelectedText == "RIGHT")
                    return Direction.Right;
                else
                    return Direction.Up;
            }
        }
    }
}
