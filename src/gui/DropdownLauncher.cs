using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Event;
using Naovigate.Event.NaoToGoal;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public partial class DropdownLauncher : UserControl
    {
        private Dictionary<string, Func<INaoEvent>> labelToEvent = new Dictionary<string, Func<INaoEvent>>()
        {
            { "Error", () => new ErrorEvent() },
            { "Failure", () => new FailureEvent(EventCode.Agent) },
            { "Success", () => new SuccessEvent(EventCode.Agent) },
            { "Agent", () => new AgentEvent() },
            { "DistanceTo", () => new DistanceToEvent(0) },
            { "Holding", () => new HoldingEvent(0) },
            { "Location", () => new LocationEvent(0) },
            { "See", () => new SeeEvent(0, 0) },
            { "State", () => new StateEvent(0) }
        };


        public DropdownLauncher()
        {
            InitializeComponent();
        }

        private void postEventButton_Click(object sender, EventArgs e)
        {
            string eventName = eventDropdown.GetItemText(eventDropdown.SelectedItem);
            INaoEvent naoEvent = labelToEvent[eventName]();
            EventQueue.Goal.Post(naoEvent);
        }


    }
}
