using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Event;
using Naovigate.Event.GoalToNao;
using Naovigate.Event.Internal;
using Naovigate.Grabbing;
using Naovigate.Movement;


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
                {radioStandUp, LaunchStandUpEvent},
                {radioGrab, LaunchGrabEvent},
                {radioPutDown, LaunchPutDownEvent},
                {radioHalt, LaunchHaltEvent},
                {radioSitDown, LaunchSitDownEvent}
            };
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            RadioButton[] radios = new RadioButton[6] {radioMove, radioStandUp, radioGrab, radioPutDown, radioHalt, radioSitDown};
            foreach (RadioButton rb in radios)
            {
                if (rb.Checked)
                {
                    eventLauncher[rb]();
                    break;
                }
            }
        }

        private void LaunchMoveEvent()
        {
            Walk.Instance.WalkWhileHolding();
        }

        private void LaunchStandUpEvent()
        {
            EventQueue.Nao.Post(new StandUpEvent());
        }

        private void LaunchSitDownEvent()
        {
            EventQueue.Nao.Post(new SitDownEvent());
        }

        private void LaunchGrabEvent()
        {
            Grabber.Instance.Grab();
        }

        private void LaunchPutDownEvent()
        {
            EventQueue.Nao.Enqueue(new PutDownEvent());
        }

        private void LaunchHaltEvent()
        {
            EventQueue.Nao.Enqueue(new HaltEvent());
        }
    }
}
