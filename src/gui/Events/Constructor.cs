using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Naovigate.Event;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A class that represents a link between an instantiator and the possible parameters it may take.
    /// It is used in order to build dynamic events with variable parameters sets.
    /// </summary>
    public sealed class Constructor
    {
        private Func<INaoEvent> instantiate;
        private IUserParameter[] parameters;

        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="instantiate">A delegate which instantiates an object.</param>
        /// <param name="parameters">Parameters the instantiator may take.</param>
        public Constructor(Func<INaoEvent> instantiate, params IUserParameter[] parameters)
        {
            this.instantiate = instantiate;
            this.parameters = parameters;
        }

        /// <summary>
        /// The instantiator.
        /// </summary>
        public Func<INaoEvent> Instantiate
        {
            get { return instantiate; }
        }

        /// <summary>
        /// The instantiator's parameters.
        /// </summary>
        public IUserParameter[] Parameters
        {
            get { return parameters; }
        }
    }

}
