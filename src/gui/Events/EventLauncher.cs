using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Event;
using Naovigate.GUI.Events.Parameters;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A class that provides the general pattern of event-launching controls.
    /// It has a drop-down to select the desired event to launch, and a button to launch it.
    /// When an event is selected, the parameter-panel is updated with fields to accomodate the
    /// selected event's possible parameters.
    /// 
    /// The control that allows the user to choose a parameter depends on the parameter type.
    /// The different types and controls are linked in pairs in an internal parameter-map.
    /// </summary>
    public partial class EventLauncher : UserControl
    {
        public event Action<Constructor, Dictionary<Type, Func<IParamChooser>>> OnEventChosen;
        
        private Dictionary<String, Constructor> eventConstructors;
        private Dictionary<Type, Func<IParamChooser>> parameterMap;

        /// <summary>
        /// Creates a new instance of this control.
        /// Already links certain types to their chooser-control in the parameter map.
        /// </summary>
        public EventLauncher()
        {
            InitializeComponent();
            parameterMap = new Dictionary<Type, Func<IParamChooser>>();
            PopulateParameterMap();
        }

        /// <summary>
        /// Returns a given parameters value as an Object.
        /// The parameter is identified by its name as a string.
        /// </summary>
        public Func<String, Object> GetParameterObject
        {
            get;
            set;
        }

        /// <summary>
        /// This dynamic function-property is used to determine whether the launcher is in the right state to launch an event.
        /// </summary>
        public Func<Constructor, Dictionary<Type, Func<IParamChooser>>, Boolean> CanPost
        {
            get;
            set;
        }   

        /// <summary>
        /// Posts the event.
        /// </summary>
        /// <param name="naoEvent">A Nao event.</param>
        protected virtual void PostEvent(INaoEvent naoEvent) { }

        /// <summary>
        /// Returns the value associated with a given parameter.
        /// </summary>
        /// <typeparam name="T">The parameter's type.</typeparam>
        /// <param name="name">The parameter's name.</param>
        /// <returns>The given parameter's value, cast to the given type.</returns>
        protected T GetParameter<T>(string name)
        {
            return (T)Convert.ChangeType(GetParameterObject(name), typeof(T));
        }

        /// <summary>
        /// Populates the parameter-control map with some default-mappings provided by this class
        /// and then with custom mappings provided by any subclasses.
        /// </summary>
        protected void PopulateParameterMap()
        {
            PopulateWithDefaults();
            PopulateWithCustoms();
        }

        /// <summary>
        /// Populates the parameter-control map with default mappings.
        /// The mappings include the types: int, float, string.
        /// </summary>
        protected void PopulateWithDefaults()
        {
            AddParameterMapping(typeof(int), () => new IntegerChooser() as IParamChooser);
            AddParameterMapping(typeof(float), () => new IntegerChooser() as IParamChooser);
            AddParameterMapping(typeof(string), () => new StringChooser() as IParamChooser);
        }

        /// <summary>
        /// Populates the parameter-control map with custom mappings provided by a subclass.
        /// </summary>
        protected virtual void PopulateWithCustoms() { }

        /// <summary>
        /// Adds a new parameter-control mapping to the map.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void AddParameterMapping(Type key, Func<IParamChooser> value)
        {
            parameterMap.Add(key, value);
        }

        /// <summary>
        /// Customizes this event launcher with custom launch-button-text and events.
        /// </summary>
        /// <param name="buttonLabel">The desired label for the launch-button.</param>
        /// <param name="builders">A dictionary linking a parameter name, to its constructor.</param>
        protected void Customize(string buttonLabel, Dictionary<String, Constructor> builders)
        {
            eventConstructors = builders;
            PopulateSelector();
            CustomizeButton(buttonLabel);
        }

        /// <summary>
        /// Populates the drop-down with dynamic-event-items representing this launcher's events.
        /// </summary>
        private void PopulateSelector()
        {
            foreach (string eventName in eventConstructors.Keys)
            {
                Constructor eventConstructor = eventConstructors[eventName];
                eventSelector.Items.Add(
                    new DynamicEventItem(eventName, eventConstructor));
            }
        }

        /// <summary>
        /// Sets the launch-button's label.
        /// </summary>
        /// <param name="label">The desired button's label.</param>
        private void CustomizeButton(string label)
        {
            postButton.Text = label;
        }

        /// <summary>
        /// Notifies any subscribers an event was chosen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            DynamicEventItem item = (eventSelector.SelectedItem as DynamicEventItem);
            if (item == null)
                return;
            OnEventChosen(item.Constructor, parameterMap);
        }

        /// <summary>
        /// Posts the given event if allowed to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void postButton_Click(object sender, EventArgs e)
        {
            DynamicEventItem item = (eventSelector.SelectedItem as DynamicEventItem);
            if (item == null)
                return;
            if (CanPost(item.Constructor, parameterMap))
                PostEvent(item.Constructor.Instantiate());
                
        }
    }
}
