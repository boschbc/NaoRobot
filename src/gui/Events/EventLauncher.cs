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
    public partial class EventLauncher : UserControl
    {
        public event Action<Constructor, Dictionary<Type, Func<IParamChooser>>> OnEventChosen;
        
        private Dictionary<String, Constructor> eventConstructors;
        private Dictionary<Type, Func<IParamChooser>> parameterMap;

        public EventLauncher()
        {
            InitializeComponent();
            parameterMap = new Dictionary<Type, Func<IParamChooser>>();
            PopulateParameterMap();
        }

        public Func<String, Object> GetParameterObject
        {
            get;
            set;
        }

        public Func<Constructor, Dictionary<Type, Func<IParamChooser>>, Boolean> CanPost
        {
            get;
            set;
        }   

        protected virtual void PostEvent(INaoEvent naoEvent) { }

        protected T GetParameter<T>(string name)
        {
            return (T)Convert.ChangeType(GetParameterObject(name), typeof(T));
        }

        protected void PopulateParameterMap()
        {
            PopulateWithDefaults();
            PopulateWithCustoms();
        }

        protected void PopulateWithDefaults()
        {
            AddParameterMapping(typeof(int), () => new IntegerChooser() as IParamChooser);
            AddParameterMapping(typeof(float), () => new IntegerChooser() as IParamChooser);
            AddParameterMapping(typeof(string), () => new StringChooser() as IParamChooser);
        }

        protected virtual void PopulateWithCustoms() { }

        protected void AddParameterMapping(Type key, Func<IParamChooser> value)
        {
            parameterMap.Add(key, value);
        }

        protected void Customize(string buttonLabel, Dictionary<String, Constructor> builders)
        {
            eventConstructors = builders;
            PopulateSelector();
            CustomizeButton(buttonLabel);
        }

        private void PopulateSelector()
        {
            foreach (string eventName in eventConstructors.Keys)
            {
                Constructor eventConstructor = eventConstructors[eventName];
                eventSelector.Items.Add(
                    new DynamicEventItem(eventName, eventConstructor));
            }
        }

        private void CustomizeButton(string label)
        {
            postButton.Text = label;
        }

        private void eventSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            DynamicEventItem item = (eventSelector.SelectedItem as DynamicEventItem);
            if (item == null)
                return;
            OnEventChosen(item.Constructor, parameterMap);
        }

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
