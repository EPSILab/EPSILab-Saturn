using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace SolarSystem.Saturn.Win8.Helpers
{
    static class BackgroundTaskRegistrationHelper
    {
        private const string BackgroundTaskName = "Refresh Tile BackgroundTask";
        private const string BackgroundTaskEntryPoint = "SolarSystem.Saturn.Win8.BackgroundTasks.RefreshTileBackgroundTask";

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
    }
}
