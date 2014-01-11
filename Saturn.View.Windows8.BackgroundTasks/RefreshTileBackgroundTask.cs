using Saturn.View.Windows8.TileFactory;
using Windows.ApplicationModel.Background;

namespace SolarSystem.Saturn.Win8.BackgroundTasks
{
    /// <summary>
    /// Background task which updates the application tile
    /// </summary>
    public sealed class RefreshTileBackgroundTask : IBackgroundTask
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
                CreateApplicationTileHelper.CreateAsync();
            }
            finally
            {
                // Informs the system task is finished
                deferral.Complete();
            }
        }
    }
}