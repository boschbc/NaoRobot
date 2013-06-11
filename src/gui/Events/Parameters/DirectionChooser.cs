using System;
using System.Windows.Forms;
using Naovigate.Navigation;
using Naovigate.Util;
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
                Logger.Log(this, "HALHASLDASLDJAS: " + value.SelectedItem + " " + value.SelectedItem.Equals("RIGHT"));
                if (value.SelectedItem.Equals("UP"))
                    return Direction.Up;
                else if (value.SelectedItem.Equals("DOWN"))
                    return Direction.Down;
                else if (value.SelectedItem.Equals("LEFT"))
                    return Direction.Left;
                else if (value.SelectedItem.Equals("RIGHT"))
                    return Direction.Right;
                else
                    return Direction.Up;
            }
        }
    }
}
