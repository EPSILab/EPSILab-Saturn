using Microsoft.Phone.Scheduler;
using SolarSystem.Saturn.WP8.Resources;

namespace SolarSystem.Saturn.WP8.Helpers.BackgroundTask
{
    class BackgroundTaskRegistrationHelper
    {
        private const string BackgroundTaskName = "CreateApplicationTileAgent";
        private const string OldBackgroundTaskName = "EPSILabScheduledAgent";

        public static void Register()
        {
            // Remove old background task
            PeriodicTask oldPeriodicTask = ScheduledActionService.Find(OldBackgroundTaskName) as PeriodicTask;

            if (oldPeriodicTask != null)
                ScheduledActionService.Remove(OldBackgroundTaskName);

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
