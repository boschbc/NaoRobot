using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Event;
using Naovigate.GUI.Popups;
using Naovigate.GUI.Popups.ParamChooser;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    public partial class EventLauncher : UserControl
    {
        private Dictionary<String, Func<INaoEvent>> eventBuilders;
        private Dictionary<Type, Func<Object>> parameterMap;

        public EventLauncher()
        {
            InitializeComponent();
            parameterMap = new Dictionary<Type, Func<Object>>();
            InitializeParameterMap();
        }

        protected virtual void InitializeParameterMap()
        {
            AddParameterMapping(typeof(int), AskUserForInteger);
            AddParameterMapping(typeof(string), AskUserForString);
        }

        protected void AddParameterMapping(Type key, Func<Object> value)
        {
            parameterMap.Add(key, value);
        }

        public void Customize(string buttonLabel, Dictionary<String, Func<INaoEvent>> builders)
        {
            InitializeEventBuilders(builders);
            PopulateSelector();
            CustomizeButton(buttonLabel);
        }

        protected virtual void PostEvent(INaoEvent e) { }

        protected T UserParameter<T>()
        {
            Object result = parameterMap[typeof(T)]();
            if (result == null)
                return default(T);
            else
                return (T) Convert.ChangeType(result, typeof(T));
        }

        protected Object DisplayPopup(IParamChooser chooser)
        {
            using (UserInputPopup popup = new UserInputPopup())
            {
                popup.SetParamChooser(chooser);
                var result = popup.ShowDialog();
                if (result == DialogResult.OK)
                    return chooser.Value;
            }
            return null;
        }

        private Object AskUserForInteger()
        {
            return DisplayPopup(new IntegerChooser());
        }

        private Object AskUserForString()
        {
            return DisplayPopup(new StringChooser());
        }

        private void InitializeEventBuilders(Dictionary<String, Func<INaoEvent>> builders)
        {
            eventBuilders = builders;
        }

        private void PopulateSelector()
        {
            foreach (string eventName in eventBuilders.Keys)
            {
                Func<INaoEvent> buildEvent = eventBuilders[eventName];
                eventSelector.Items.Add(
                    new ComboboxItem(eventName, buildEvent));
            }
        }

        private void CustomizeButton(string label)
        {
            postButton.Text = label;
        }

        private void postButton_Click(object sender, EventArgs e)
        {
            ComboboxItem item = (eventSelector.SelectedItem as ComboboxItem);
            if (item == null)
                return;
            INaoEvent naoEvent = item.Value();
            PostEvent(naoEvent);
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public Func<INaoEvent> Value { get; set; }

        public ComboboxItem(string text, Func<INaoEvent> value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }

}
