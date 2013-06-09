using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Event;

namespace Naovigate.GUI.Events
{
    public class Constructor
    {
        private Func<INaoEvent> instantiate;
        private IUserParameter[] parameters;

        public Constructor(Func<INaoEvent> instantiate, params IUserParameter[] parameters)
        {
            this.instantiate = instantiate;
            this.parameters = parameters;
        }

        public Func<INaoEvent> Instantiate
        {
            get { return instantiate; }
        }

        public IUserParameter[] Parameters
        {
            get { return parameters; }
        }
    }

}
