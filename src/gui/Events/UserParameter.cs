using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.GUI.Events
{
    public class UserParameter<T> : IUserParameter
    {
        public UserParameter(string name)
        {
            Name = name;
            Type = typeof(T);
        }

        public string Name
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }

        public IParamChooser Chooser
        {
            get;
            set;
        }
    }

}
