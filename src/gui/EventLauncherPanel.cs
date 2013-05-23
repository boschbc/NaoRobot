using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using Naovigate.Event;
using Naovigate.Grabbing;

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
                {radioGrab, LaunchGrabEvent},
                {radioPutDown, LaunchPutDownEvent}
            };
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            RadioButton[] radios = new RadioButton[4] {radioMove, radioLook, radioGrab, radioPutDown};
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
            EventQueue.Nao.Post(moveEvent);
        }

        private void LaunchLookEvent()
        {

        }

        private void LaunchGrabEvent()
        {
            CoolGrabber.Instance.doSomething();
        }

        private void LaunchPutDownEvent()
        {
            Grabber.Instance.PutDown();
        }
    }
}
