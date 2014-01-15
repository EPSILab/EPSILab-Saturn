using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Tiles;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Toasts;
using System.Windows;

namespace SolarSystem.Saturn.View.WindowsPhone.SheduledTaskAgent
{
    /// <summary>
    /// An agent which create the application tile every 30 minutes
    /// </summary>
    public class CreateApplicationTileAgent : ScheduledTaskAgent
    {
        #region Constructors

        /// <summary>
        /// Static constructor. Handle errors
        /// </summary>
        static CreateApplicationTileAgent()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => Application.Current.UnhandledException += UnhandledException);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raised when an unmanaged error occured
        /// </summary>
        /// <param name="sender">Element which raised the event</param>
        /// <param name="e">Informations about exception</param>
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        /// <summary>
        /// Create the application tile
        /// </summary>
        /// <param name="task">Informations about the periodic task</param>
        protected override async void OnInvoke(ScheduledTask task)
        {
            try
            {
                // Update application tile
                await ApplicationTileManager.Update();

                // Display toast notification if a new element has been published
                await DisplayLastNewsToast.CheckAndDisplay();
                await ConferenceToastManager.CheckAndDisplay();
                await DisplayLastShowToast.CheckAndDisplay();
            }
            finally
            {
                NotifyComplete();
            }
        }

        #endregion
    }
}