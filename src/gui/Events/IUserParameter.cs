using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.GUI.Events
{
    public interface IUserParameter
    {
        string Name
        {
            get;
        }

        Type Type
        {
            get;
        }

        IParamChooser Chooser
        {
            get;
        }
    }
}
