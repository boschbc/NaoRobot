using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Event;
using Naovigate.Event.GoalToNao;
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
                {radioHalt, LaunchHaltEvent}
            };
        }

        private void launchButton_Click(object sender, EventArgs e)
        {
            RadioButton[] radios = new RadioButton[5] {radioMove, radioStandUp, radioGrab, radioPutDown, radioHalt};
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
            Pose.Instance.StandUp();
        }

        private void LaunchGrabEvent()
        {
            CoolGrabber.Instance.doSomething();
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
