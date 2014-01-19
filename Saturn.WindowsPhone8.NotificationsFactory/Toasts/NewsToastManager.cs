using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel;
using Microsoft.Phone.Shell;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Resources;
using System;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.View.WindowsPhone.TileFactory.Toasts
{
    /// <summary>
    /// Display a toast notification when a news has been published
    /// </summary>
    public static class NewsToastManager
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public static async Task CheckAndToast()
        {
            // Resolve model
            IReadableLimitable<News> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<News>>();

            // Get last news Id from model
            int idLastNews = await model.GetLastInsertedId();

            // Get last news saved Id
            int idLastNewsSaved = IsolatedStorageSettings.ApplicationSettings.Contains(LibResources.NewsStorageKey) ? (int)IsolatedStorageSettings.ApplicationSettings[LibResources.NewsStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastNews != idLastNewsSaved)
            {
                News lastNews = await model.GetAsync(idLastNews);

                ShellToast toast = new ShellToast
                {
                    Title = LibResources.NewNews,
                    Content = lastNews.Titre,
                    NavigationUri = new Uri(string.Format("/NewsPage.xaml?Id={0}", lastNews.Code_News), UriKind.Relative)
                };

                toast.Show();

                IsolatedStorageSettings.ApplicationSettings[LibResources.NewsStorageKey] = idLastNews;
            }
        }
    }
}