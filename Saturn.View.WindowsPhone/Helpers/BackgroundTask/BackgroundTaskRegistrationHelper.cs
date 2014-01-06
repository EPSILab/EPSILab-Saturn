using Microsoft.Phone.Scheduler;
using SolarSystem.Saturn.View.WindowsPhone.Resources;

namespace SolarSystem.Saturn.View.WindowsPhone.Helpers.BackgroundTask
{
    /// <summary>
    /// Register the periodic task to update the application tile
    /// </summary>
    static class BackgroundTaskRegistrationHelper
    {
        private const string BackgroundTaskName = "CreateApplicationTileAgent";

        public static void Register()
        {
            // See if task already exists. If it does, we delete it
            PeriodicTask periodicTask = ScheduledActionService.Find(BackgroundTaskName) as PeriodicTask;

            if (periodicTask != null)
                ScheduledActionService.Remove(BackgroundTaskName);

            // Now we (re)create the background task
            periodicTask = new PeriodicTask(BackgroundTaskName)
            {
                Description = AppResources.TASK_DESCRIPTION
            };

            // Register to task to the system
            ScheduledActionService.Add(periodicTask);

            // If Debug mode, lauch the task immediatly
#if DEBUG_AGENT
                ScheduledActionService.LaunchForTest(BackgroundTaskName, TimeSpan.FromSeconds(10));
#endif
        }
    }
}