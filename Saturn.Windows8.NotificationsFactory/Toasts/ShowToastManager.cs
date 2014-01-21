using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using NotificationsExtensions.ToastContent;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI.Notifications;

namespace EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Toasts
{
    /// <summary>
    /// A class which checks from the show if there is a new show and display a toast notification
    /// </summary>
    public static class ShowToastManager
    {
        /// <summary>
        /// Storage key
        /// </summary>
        private const string StorageKey = "LastShowSavedId";

        /// <summary>
        /// CHeck if a new show is a available and display the toast notification
        /// </summary>
        public static async void CheckAndDisplay()
        {
            // Resolve model
            IReadableLimitable<Salon> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Salon>>();

            // Get last show Id from model
            int idLastShow = await model.GetLastInsertedId();

            // Get last show saved Id
            int idLastShowSaved = 0;

            if (ApplicationData.Current.LocalSettings.Values[StorageKey] != null)
            {
                idLastShowSaved = (int)ApplicationData.Current.LocalSettings.Values[StorageKey];
            }

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastShow != idLastShowSaved)
            {
                ResourceLoader resourceLoader = new ResourceLoader();
                Salon show = await model.GetAsync(idLastShow);

                IToastImageAndText04 toastContent = ToastContentFactory.CreateToastImageAndText04();

                toastContent.Image.Src = show.Image;
                toastContent.Image.Alt = show.Nom;

                toastContent.TextHeading.Text = resourceLoader.GetString("Shows_New");
                toastContent.TextBody1.Text = show.Nom;
                toastContent.TextBody2.Text = string.Format(resourceLoader.GetString("Shows_DateFormat"), show.Date_Heure_Debut, show.Date_Heure_Fin);

                ToastNotification toast = toastContent.CreateNotification();
                ToastNotificationManager.CreateToastNotifier().Show(toast);

                ApplicationData.Current.LocalSettings.Values[StorageKey] = idLastShow;
            }
        }
    }
}