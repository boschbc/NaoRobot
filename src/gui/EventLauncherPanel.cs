using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Naovigate.Event;

namespace Naovigate.GUI
{
    public partial class EventLauncherPanel : UserControl
    {
        private Dictionary<RadioButton, Action> eventLauncher;

        public EventLauncherPanel()
        {
            InitializeComponent();
            eventLauncher = new Dictionary<RadioButton, Action>()
            {
                {radioMove, LaunchMoveEvent},
                {radioLook, LaunchLookEvent},
                {radioGrab, LaunchGrabEvent}
            };
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            RadioButton[] radios = new RadioButton[3] {radioMove, radioLook, radioGrab};
            foreach (RadioButton rb in radios)
            {
                if (rb.Checked)
                {
                    eventLauncher[rb]();
                }
            }
        }

        private void LaunchMoveEvent()
        {
            MoveNaoEvent moveEvent = new MoveNaoEvent(0.5f, 0.0f);
            EventQueue.Instance.Post(moveEvent);
        }

        private void LaunchLookEvent()
        {
            LookNaoEvent lookEvent = new LookNaoEvent(3.14f);
            lookEvent.Fire();
        }

        private void LaunchGrabEvent()
        {
            GrabNaoEvent grabEvent = new GrabNaoEvent();
            grabEvent.Fire();
        }
    }
}
