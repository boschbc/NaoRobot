using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Event;
using Naovigate.Util;

namespace Naovigate.GUI.Events
{
    public partial class EventLauncher : UserControl
    {
        private List<Func<INaoEvent>> eventBuilders;

        public EventLauncher()
        {
            InitializeComponent();
        }

        public void Customize(string buttonLabel, IEnumerable<Func<INaoEvent>> builders)
        {
            InitializeEventBuilders(builders);
            PopulateSelector();
            CustomizeButton(buttonLabel);
        }

        protected virtual void PostEvent(INaoEvent e) { }

        private void InitializeEventBuilders(IEnumerable<Func<INaoEvent>> builders)
        {
            eventBuilders = new List<Func<INaoEvent>>(builders);
        }

        private void PopulateSelector()
        {
            foreach (Func<INaoEvent> buildEvent in eventBuilders)
            {
                eventSelector.Items.Add(
                    new ComboboxItem(buildEvent().ToString(), buildEvent));
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
            INaoEvent event_ = item.Value();
            PostEvent(event_);
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
