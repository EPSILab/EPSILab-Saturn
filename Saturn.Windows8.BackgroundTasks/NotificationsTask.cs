using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory;
using Windows.ApplicationModel.Background;

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
                ApplicationTileManager.Create();
            }
            finally
            {
                // Informs the system task is finished
                deferral.Complete();
            }
        }
    }
}