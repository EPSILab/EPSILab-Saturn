using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory;

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

        /// <summary>
        /// Normal constructor. Resolve NInject dependencies.
        /// </summary>
        /// <param name="model">Access to the news model</param>
        public CreateApplicationTileAgent(IReadableLimitable<News> model)
        {
            _model = model;
        }

        #endregion

        #region Attributes

        private readonly IReadableLimitable<News> _model;

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
                await CreateApplicationTileHelper.Create(_model);
            }
            finally
            {
                NotifyComplete();
            }
        }

        #endregion
    }
}