using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Resources;
using NotificationsExtensions.ToastContent;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Notifications;

namespace EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Toasts
{
    /// <summary>
    /// A class which checks from the show if there is a new show and display a toast notification
    /// </summary>
    public class ShowToastManager : ToastManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ShowToastManager()
            : base("LastShowSavedId")
        {
        }

        /// <summary>
        /// CHeck if a new show is a available and display the toast notification
        /// </summary>
        public override async Task CheckAndDisplayAsync()
        {
            // Resolve model
            IReadableLimitable<Show> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Show>>();

            // Get last show Id from model
            int idLastShow = await model.GetLastInsertedId();

            // Get last show saved Id
            int idLastShowSaved = 0;

            if (ApplicationData.Current.LocalSettings.Values[_storageKey] != null)
            {
                idLastShowSaved = (int)ApplicationData.Current.LocalSettings.Values[_storageKey];
            }

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastShow != idLastShowSaved)
            {
                Show show = await model.GetAsync(idLastShow);

                IToastImageAndText04 toastContent = ToastContentFactory.CreateToastImageAndText04();

                toastContent.Image.Src = show.ImageUrl;
                toastContent.Image.Alt = show.Name;

                toastContent.TextHeading.Text = ResourcesAccessor.GetString("Shows_New");
                toastContent.TextBody1.Text = show.Name;
                toastContent.TextBody2.Text = string.Format(ResourcesAccessor.GetString("Shows_DateFormat"), show.Start_DateTime, show.End_DateTime);

                ToastNotification toast = toastContent.CreateNotification();
                ToastNotificationManager.CreateToastNotifier().Show(toast);

                ApplicationData.Current.LocalSettings.Values[_storageKey] = idLastShow;
            }
        }
    }
}