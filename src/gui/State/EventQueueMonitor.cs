using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Event;
using Naovigate.Event.NaoToGoal;
using Naovigate.Event.GoalToNao;
using Naovigate.Util;

namespace Naovigate.GUI.State
{
    public partial class EventQueueMonitor : UserControl
    {
        private static Dictionary<Type, Color> eventToColor = new Dictionary<Type, Color>() {
            { typeof(FailureEvent), Color.Red },
            { typeof(SuccessEvent), Color.Green },
            { typeof(ErrorEvent), Color.Blue } 
        };

        public EventQueueMonitor()
        {
            EventQueue.Nao.SubscribeFire(UpdateNaoQueueInfo);
            EventQueue.Goal.SubscribeFire(UpdateGoalQueueInfo);
            InitializeComponent();
        }

        public void UpdateNaoQueueInfo(INaoEvent e)
        {
            if (naoEventLabel.InvokeRequired)
            {
                naoEventLabel.Invoke(new MethodInvoker(() => UpdateNaoQueueInfo(e)));
            }
            naoEventLabel.Text = e.ToString();
        }

        public void UpdateGoalQueueInfo(INaoEvent e)
        {
            if (goalEventLabel.InvokeRequired)
            {
                goalEventLabel.Invoke(new MethodInvoker(() => UpdateGoalQueueInfo(e)));
            }
            goalEventLabel.Text = e.ToString();
            Type t = e.GetType();
            if (eventToColor.ContainsKey(t))
                goalEventLabel.ForeColor = eventToColor[t];
            else
                goalEventLabel.ForeColor = Color.Black;
        }
    }
}
