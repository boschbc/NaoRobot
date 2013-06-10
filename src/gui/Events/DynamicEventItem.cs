using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A class intended as a drop-down item. It links an event constructor to a label.
    /// </summary>
    public sealed class DynamicEventItem
    {
        public string Text { get; set; }
        public Constructor Constructor { get; set; }

        /// <summary>
        /// Creates a new dynamic event item.
        /// </summary>
        /// <param name="text">The label associated with this event-item.</param>
        /// <param name="constructor">The event's constructor.</param>
        public DynamicEventItem(string text, Constructor constructor)
        {
            Text = text;
            Constructor = constructor;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
