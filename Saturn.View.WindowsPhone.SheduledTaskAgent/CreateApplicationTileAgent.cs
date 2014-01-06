using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory;

namespace SolarSystem.Saturn.View.WindowsPhone.SheduledTaskAgent
{
    /// <summary>
    /// An agent which create the application tile every 30 minutes
    /// </summary>
    public class CreateApplicationTileAgent : ScheduledTaskAgent
    {
        #region Constructor

        /// <summary>
        /// Constructor
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
                await CreateApplicationTileHelper.Create();
            }
            finally
            {
                NotifyComplete();
            }
        }

        #endregion
    }
}