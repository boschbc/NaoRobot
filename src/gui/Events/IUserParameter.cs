using System;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// An interface that links between a parameter's name, type and the control that allows the user to choose it.
    /// </summary>
    public interface IUserParameter
    {
        /// <summary>
        /// The parameter's name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// The parameter's type.
        /// </summary>
        Type Type
        {
            get;
        }

        /// <summary>
        /// The parameter's chooser.
        /// A chooser is a control which allows the choice of this parameter.
        /// </summary>
        IParamChooser Chooser
        {
            get;
        }
    }
}
