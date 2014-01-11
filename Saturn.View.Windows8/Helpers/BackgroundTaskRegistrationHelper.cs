using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace SolarSystem.Saturn.Win8.Helpers
{
    /// <summary>
    /// Register 
    /// </summary>
    static class BackgroundTaskRegistrationHelper
    {
        #region Attributes

        /// <summary>
        /// Background task name
        /// </summary>
        private const string BackgroundTaskName = "Refresh Tile BackgroundTask";

        /// <summary>
        /// Background task entry point (Assembly and task)
        /// </summary>
        private const string BackgroundTaskEntryPoint = "SolarSystem.Saturn.View.Windows8.BackgroundTasks.RefreshTileBackgroundTask";

        #endregion

        #region Methods

        /// <summary>
        /// Register the background task
        /// </summary>
        public async static Task RegisterAsync()
        {
            BackgroundAccessStatus status = BackgroundExecutionManager.GetAccessStatus();

            if (status == BackgroundAccessStatus.Unspecified)
            {
                BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

                if (backgroundAccessStatus != BackgroundAccessStatus.Denied)
                {
                    foreach (var task in BackgroundTaskRegistration.AllTasks)
                    {
                        if (task.Value.Name == BackgroundTaskName)
                        {
                            task.Value.Unregister(true);
                        }
                    }

                    var taskBuilder = new BackgroundTaskBuilder
                    {
                        Name = BackgroundTaskName,
                        TaskEntryPoint = BackgroundTaskEntryPoint
                    };

                    IBackgroundTrigger trigger;

                    if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                        backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
                    {
                        trigger = new TimeTrigger(30, false);
                    }
                    else
                    {
                        trigger = new MaintenanceTrigger(30, false);
                    }

                    taskBuilder.SetTrigger(trigger);
                    taskBuilder.Register();
                }
            }
        }

        #endregion
    }
}