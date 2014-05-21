using EPSILab.SolarSystem.Saturn.WindowsPhone8.TileFactory.Tiles;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.TileFactory.Toasts;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using System.Windows;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.BackgroundTasks
{
    /// <summary>
    /// An agent which create the application tile every 30 minutes
    /// </summary>
    public class CreateApplicationTileAgent : ScheduledTaskAgent
    {
        #region Constructor

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
                ApplicationTileManager appTileManager = new ApplicationTileManager();
                await appTileManager.UpdateAsync();

                // Display toast notification if a new element has been published
                ToastManager manager = new NewsToastManager();
                await manager.CheckAndToastAsync();

                manager = new ConferenceToastManager();
                await manager.CheckAndToastAsync();

                manager = new ShowToastManager();
                await manager.CheckAndToastAsync();
            }
            finally
            {
                NotifyComplete();
            }
        }

        #endregion
    }
}