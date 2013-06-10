using System;

namespace Naovigate.GUI.Events
{
    /// <summary>
    /// An interface that represents a class with a general value.
    /// </summary>
    public interface IParamChooser
    {
        /// <summary>
        /// This chooser's value.
        /// </summary>
        Object Value
        {
            get;
        }
    }
}
