using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.TileFactory.Toasts
{
    /// <summary>
    /// Base class for toast managers
    /// </summary>
    public abstract class ToastManager
    {
        /// <summary>
        /// Abstract method for checking if a new element is available and for displaying a toast notification
        /// </summary>
        public abstract Task CheckAndToastAsync();
    }
}