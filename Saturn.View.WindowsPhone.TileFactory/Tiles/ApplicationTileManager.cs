using Autofac;
using Microsoft.Phone.Shell;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Resources;
using SolarSystem.Saturn.ViewModel;
using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.View.WindowsPhone.TileFactory.Tiles
{
    /// <summary>
    /// A helper to create the application tile
    /// </summary>
    public static class ApplicationTileManager
    {
        /// <summary>
        /// Create the application tile by calling the webservice
        /// </summary>
        public static async Task Update()
        {
            IReadableLimitable<News> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<News>>();

            // Get last news Id
            int idLastNews = await model.GetLastInsertedId();

            // Get last news Id saved in local storage
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            int idLastNewsSaved = settings.Contains(LibResources.NewsStorageKey) ? (int)settings[LibResources.NewsStorageKey] : 0;

            // Compare the 2 codes. If they are differents, update the tile
            if (idLastNews != idLastNewsSaved)
            {
                News lastNews = await model.GetAsync(idLastNews);

                // The application tile is the first active tile, even if it's not pinned
                ShellTile existingTile = ShellTile.ActiveTiles.First();

                ShellTileData newTile = new FlipTileData
                {
                    BackgroundImage = new Uri(lastNews.Image, UriKind.Absolute),
                    BackContent = lastNews.Titre,
                    BackTitle = lastNews.Date_Heure.ToString("g")
                };

                // Update the tile
                existingTile.Update(newTile);

                settings[LibResources.NewsStorageKey] = idLastNews;
            }
        }
    }
}