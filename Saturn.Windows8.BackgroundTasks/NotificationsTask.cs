using System;
using Windows.ApplicationModel.Background;
using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Tiles;
using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Toasts;

namespace EPSILab.SolarSystem.Saturn.Windows8.BackgroundTasks
{
    /// <summary>
    /// Background task which updates the application tile
    /// </summary>
    public sealed class NotificationsTask : IBackgroundTask
    {
        /// <summary>
        /// Run the background task
        /// </summary>
        /// <param name="taskInstance">Returns task informations</param>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            try
            {
                // Update the application tile
                ApplicationTileManager.Create();

                //// Check is new elements have been published and displays a toast notification
                //ConferenceToastManager.CheckAndDisplay();
                //NewsToastManager.CheckAndDisplay();
                //ShowToastManager.CheckAndDisplay();
            }
            finally
            {
                // Informs the system task is finished
                deferral.Complete();
            }
        }
    }
}