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
    /// A class which checks from the model if there is a new news and display a toast notification
    /// </summary>
    public class NewsToastManager : ToastManager
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NewsToastManager()
            : base("LastNewsSavedId")
        {
        }

        /// <summary>
        /// CHeck if a new news is a available and display the toast notification
        /// </summary>
        public override async Task CheckAndDisplayAsync()
        {
            // Resolve model
            IReadableLimitable<News> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<News>>();

            // Get last news Id from model
            int idLastNews = await model.GetLastInsertedId();

            // Get last news saved Id
            int idLastNewsSaved = 0;

            if (ApplicationData.Current.LocalSettings.Values[_storageKey] != null)
            {
                idLastNewsSaved = (int)ApplicationData.Current.LocalSettings.Values[_storageKey];
            }

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastNews != idLastNewsSaved)
            {
                News news = await model.GetAsync(idLastNews);

                IToastImageAndText04 toastContent = ToastContentFactory.CreateToastImageAndText04();

                toastContent.Image.Src = news.ImageUrl;
                toastContent.Image.Alt = news.Title;

                toastContent.TextHeading.Text = ResourcesAccessor.GetString("News_New");
                toastContent.TextBody1.Text = news.Title;
                toastContent.TextBody2.Text = news.ShortText;

                ToastNotification toast = toastContent.CreateNotification();
                ToastNotificationManager.CreateToastNotifier().Show(toast);

                ApplicationData.Current.LocalSettings.Values[_storageKey] = idLastNews;
            }
        }
    }
}