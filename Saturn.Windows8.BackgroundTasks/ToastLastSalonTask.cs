using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Toasts;
using Windows.ApplicationModel.Background;

namespace EPSILab.SolarSystem.Saturn.Windows8.BackgroundTasks
{
    /// <summary>
    /// Background task which display a toast notification if a new event is available
    /// </summary>
    public sealed class ToastLastShowTask : IBackgroundTask
    {
        /// <summary>
        /// Run the background task
        /// </summary>
        /// <param name="taskInstance">Returns task informations</param>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            try
            {
                ToastManager manager = new ShowToastManager();
                await manager.CheckAndDisplayAsync();
            }
            finally
            {
                // Informs the system task is finished
                deferral.Complete();
            }
        }
    }
}