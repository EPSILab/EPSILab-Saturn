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
    /// A class which checks from the model if there is a new news and display a toast notification
    /// </summary>
    public static class NewsToastManager
    {
        /// <summary>
        /// Storage key
        /// </summary>
        private const string StorageKey = "LastNewsSavedId";

        /// <summary>
        /// CHeck if a new news is a available and display the toast notification
        /// </summary>
        public static async void CheckAndDisplay()
        {
            // Resolve model
            IReadableLimitable<News> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<News>>();

            // Get last news Id from model
            int idLastNews = await model.GetLastInsertedId();

            // Get last news saved Id
            int idLastNewsSaved = 0;

            if (ApplicationData.Current.LocalSettings.Values[StorageKey] != null)
            {
                idLastNewsSaved = (int)ApplicationData.Current.LocalSettings.Values[StorageKey];
            }

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastNews != idLastNewsSaved)
            {
                ResourceLoader resourceLoader = new ResourceLoader();
                News news = await model.GetAsync(idLastNews);

                IToastImageAndText04 toastContent = ToastContentFactory.CreateToastImageAndText04();

                toastContent.Image.Src = news.Image;
                toastContent.Image.Alt = news.Titre;


                toastContent.TextHeading.Text = resourceLoader.GetString("News_New");
                toastContent.TextBody1.Text = news.Titre;
                toastContent.TextBody2.Text = news.Texte_Court;

                ToastNotification toast = toastContent.CreateNotification();
                ToastNotificationManager.CreateToastNotifier().Show(toast);

                ApplicationData.Current.LocalSettings.Values[StorageKey] = idLastNews;
            }
        }
    }
}