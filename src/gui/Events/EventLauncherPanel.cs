using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Event;
using Naovigate.GUI.Events;

namespace Naovigate.GUI.Panels
{
    public partial class EventLauncherPanel : UserControl
    {
        private Constructor chosenEventConstructor;

        public EventLauncherPanel()
        {
            InitializeComponent();
            HookLaunchers();
        }

        private Func<INaoEvent> MakeEvent
        {
            get;
            set;
        }

        private void HookLaunchers()
        {
            List<EventLauncher> launchers = new List<EventLauncher>()
            {
                goalEventLauncher, naoEventLauncher, internalEventLauncher
            };

            foreach (EventLauncher launcher in launchers)
            {
                launcher.OnEventChosen += HandleEventChosen;
                launcher.GetParameterObject = parameterPanel.GetParameterValue;
                launcher.CanPost = PostEvent;
            }
        }

        private bool PostEvent(Constructor constructor, 
                               Dictionary<Type, Func<IParamChooser>> chooserMap)
        {
            if (chosenEventConstructor == null)
                return false;
            else if (chosenEventConstructor != constructor)
            {
                HandleEventChosen(constructor, chooserMap);
                return false;
            }
            else
                return true;
        }

        private void AddParameters(IUserParameter[] parameters,
                                   Dictionary<Type, Func<IParamChooser>> chooserMap)
        {
            foreach (IUserParameter p in parameters)
                parameterPanel.AddParameter(p.Name, chooserMap[p.Type]());
        }

        private void HandleEventChosen(Constructor constructor, 
                                       Dictionary<Type, Func<IParamChooser>> chooserMap)
        {
            parameterPanel.ClearAllParameters();
            chosenEventConstructor = constructor;
            AddParameters(constructor.Parameters, chooserMap);
        }
    }
}
