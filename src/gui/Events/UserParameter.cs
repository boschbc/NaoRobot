using System;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// A simple implementation of the IUserParameter interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class UserParameter<T> : IUserParameter
    {
        /// <summary>
        /// Creates a new user-parameter with given name and type.
        /// </summary>
        /// <param name="name">The parameter's name.</param>
        public UserParameter(string name)
        {
            Name = name;
            Type = typeof(T);
        }

        /// <summary>
        /// Returns the parameter's name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the paramter's type.
        /// </summary>
        public Type Type
        {
            get;
            set;
        }

        /// <summary>
        /// Returns the parameter's chooser.
        /// A chooser is a control that allows the user to choose a given parameter.
        /// </summary>
        public IParamChooser Chooser
        {
            get;
            set;
        }
    }

}
