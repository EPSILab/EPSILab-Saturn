using EPSILab.SolarSystem.Saturn.WindowsPhone8.Resources;
using Microsoft.Phone.Scheduler;
using System;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.Helpers.BackgroundTask
{
    /// <summary>
    /// Register the periodic task to update the application tile
    /// </summary>
    class BackgroundTaskRegistrationHelper
    {
        /// <summary>
        /// Task name
        /// </summary>
        const string BackgroundTaskName = "CreateApplicationTileAgent";

        /// <summary>
        /// Register background task
        /// </summary>
        public void Register()
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
#if DEBUG
            ScheduledActionService.LaunchForTest(BackgroundTaskName, TimeSpan.FromSeconds(10));
#endif
        }
    }
}