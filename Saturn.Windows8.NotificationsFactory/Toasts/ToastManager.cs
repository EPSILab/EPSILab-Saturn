using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Toasts
{
    /// <summary>
    /// Base class for toast managers
    /// </summary>
    public abstract class ToastManager
    {
        /// <summary>
        /// Storage Key
        /// </summary>
        protected readonly string _storageKey;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="storageKey"></param>
        protected ToastManager(string storageKey)
        {
            _storageKey = storageKey;
        }

        /// <summary>
        /// Abstract method for checking if a new element is available and for displaying a toast notification
        /// </summary>
        public abstract Task CheckAndDisplayAsync();
    }
}