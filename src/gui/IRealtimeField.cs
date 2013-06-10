
namespace Naovigate.GUI
{
    /// <summary>
    /// An interface specifying a class can be updated in real time.
    /// </summary>
    interface IRealtimeField
    {
        /// <summary>
        /// Updates the control's contents.
        /// </summary>
        void UpdateContent();

        /// <summary>
        /// Resets the control's contents.
        /// </summary>
        void ResetContent();
    }
}
