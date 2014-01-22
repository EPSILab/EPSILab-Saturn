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
    /// A class which checks from the model if there is a new conference and display a toast notification
    /// </summary>
    public class ConferenceToastManager : ToastManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConferenceToastManager()
            : base("LastConferenceSavedId")
        {
        }

        /// <summary>
        /// CHeck if a new conference is a available and display the toast notification
        /// </summary>
        public override async Task CheckAndDisplayAsync()
        {
            // Resolve model
            IReadableLimitable<Conference> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Conference>>();

            // Get last conference Id from model
            int idLastConference = await model.GetLastInsertedId();

            // Get last conference saved Id
            int idLastConferenceSaved = 0;

            if (ApplicationData.Current.LocalSettings.Values[_storageKey] != null)
            {
                idLastConferenceSaved = (int)ApplicationData.Current.LocalSettings.Values[_storageKey];
            }

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastConference != idLastConferenceSaved)
            {
                Conference conference = await model.GetAsync(idLastConference);

                IToastImageAndText04 toastContent = ToastContentFactory.CreateToastImageAndText04();

                toastContent.Image.Src = conference.Image;
                toastContent.Image.Alt = conference.Nom;

                toastContent.TextHeading.Text = ResourcesAccessor.GetString("Conferences_New");
                toastContent.TextBody1.Text = conference.Nom;
                toastContent.TextBody2.Text = string.Format(ResourcesAccessor.GetString("Conferences_DateFormat"), conference.Date_Heure_Debut, conference.Date_Heure_Fin);

                ToastNotification toast = toastContent.CreateNotification();
                ToastNotificationManager.CreateToastNotifier().Show(toast);

                ApplicationData.Current.LocalSettings.Values[_storageKey] = idLastConference;
            }
        }
    }
}