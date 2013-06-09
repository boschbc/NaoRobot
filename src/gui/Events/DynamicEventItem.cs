using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.GUI.Events
{
    public class DynamicEventItem
    {
        public string Text { get; set; }
        public Constructor Constructor { get; set; }

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
