using Autofac;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Helpers;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Resources;
using SolarSystem.Saturn.ViewModel;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.View.WindowsPhone.TileFactory.Toasts
{
    /// <summary>
    /// Display a toast notification when a news has been published
    /// </summary>
    public static class DisplayLastNewsToast
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public static async Task CheckAndDisplay()
        {
            // Resolve model
            IReadableLimitable<News> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<News>>();

            // Get last news Id from model
            int idLastNews = await model.GetLastInsertedId();

            // Get last news saved Id
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            int idLastNewsSaved = settings.Contains(LibResources.NewsStorageKey) ? (int)settings[LibResources.NewsStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastNews != idLastNewsSaved)
            {
                News lastNews = await model.GetAsync(idLastNews);
                DisplayToastHelper.Display(lastNews.Titre);

                settings[LibResources.NewsStorageKey] = idLastNews;
            }
        }
    }
}