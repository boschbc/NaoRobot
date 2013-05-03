using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.Event
{
    /*
    * Gives an event a Priority
    */
    public enum Priority
    {
        Low = 0,
        Medium = Low + 1,
        High = Medium + 1
    };
}
