using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A control that allows the launch of Nao, Goal and internal events.
    /// The panel also contains a parameter choosing box that changes dynamically based on the chosen event.
    /// </summary>
    public sealed partial class EventLauncherPanel : UserControl
    {
        private Constructor chosenEventConstructor;

        /// <summary>
        /// Creates the control and hooks the launchers to its methods.
        /// </summary>
        public EventLauncherPanel()
        {
            InitializeComponent();
            HookLaunchers();
        }

        /// <summary>
        /// Hooks the OnEventChosen, GetParameterObject and CanPost methods of the launchers to this control's logic.
        /// </summary>
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

        /// <summary>
        /// Posts an event.
        /// If the parameter-panel does not contain the event selected, it will display the event's
        /// parameters and not post the event.
        /// If the parameter-panel already displays this event's parameters of if the event does not require
        /// any parameters, posts the event immediatly using the launcher's own PostEvent method.
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="chooserMap"></param>
        /// <returns></returns>
        private bool PostEvent(Constructor constructor, 
                               Dictionary<Type, Func<IParamChooser>> chooserMap)
        {
            if (chosenEventConstructor == null)
                return false;
            else if (chosenEventConstructor != constructor)
            {
                HandleEventChosen(constructor, chooserMap);
                if (constructor.Parameters.Length  == 0)
                    return true;
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Adds parameters to the parameters panel.
        /// </summary>
        /// <param name="parameters">A list of parameters.</param>
        /// <param name="chooserMap">A mapping between a type to a function that instantiates a new chooser-control.</param>
        private void AddParameters(IUserParameter[] parameters,
                                   Dictionary<Type, Func<IParamChooser>> chooserMap)
        {
            foreach (IUserParameter p in parameters)
                parameterPanel.AddParameter(p.Name, chooserMap[p.Type]());
        }

        /// <summary>
        /// Displays the chosen event's parameters in the parameter panel.
        /// </summary>
        /// <param name="constructor">The event's constructor.</param>
        /// <param name="chooserMap">A mapping between a type to a function that instantiates a new chooser-control.</param>
        private void HandleEventChosen(Constructor constructor, 
                                       Dictionary<Type, Func<IParamChooser>> chooserMap)
        {
            parameterPanel.ClearAllParameters();
            chosenEventConstructor = constructor;
            AddParameters(constructor.Parameters, chooserMap);
        }
    }
}
