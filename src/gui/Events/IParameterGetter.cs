using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.GUI.Events
{
    public interface IParameterGetter
    {
        T GetParameter<T>();
    }
}
