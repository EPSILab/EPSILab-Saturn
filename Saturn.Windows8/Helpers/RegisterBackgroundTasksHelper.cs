using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace EPSILab.SolarSystem.Saturn.Windows8.Helpers
{
    /// <summary>
    /// Register all application background tasks
    /// </summary>
    class RegisterBackgroundTasksHelper
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RegisterBackgroundTasksHelper()
        {
            const string projectName = "EPSILab.SolarSystem.Saturn.Windows8.BackgroundTasks";

            _tasks = new Dictionary<string, string>
            {
                { "Update application tile", string.Format("{0}.ApplicationTileUpdateTask", projectName) },
                { "Check last conference", string.Format("{0}.ToastLastConferenceTask", projectName) },
                { "Check last news", string.Format("{0}.ToastLastNewsTask", projectName) },
                { "Check last show", string.Format("{0}.ToastLastSalonTask", projectName) }
            };
        }

        /// <summary>
        /// Tasks dictionary
        /// </summary>
        private readonly IDictionary<string, string> _tasks;

        /// <summary>
        /// Register the background task
        /// </summary>
        public async Task RegisterAsync()
        {
            BackgroundAccessStatus status = BackgroundExecutionManager.GetAccessStatus();

            if (status == BackgroundAccessStatus.Unspecified)
            {
                BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();

                if (backgroundAccessStatus != BackgroundAccessStatus.Denied)
                {
                    // Delete all tasks
                    foreach (var registeredTask in BackgroundTaskRegistration.AllTasks)
                    {
                        registeredTask.Value.Unregister(true);
                    }

                    // Register new tasks
                    foreach (var task in _tasks)
                    {
                        BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder
                        {
                            Name = task.Key,
                            TaskEntryPoint = task.Value
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
}