using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.TileFactory.Resources;
using Microsoft.Phone.Shell;
using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.TileFactory.Tiles
{
    /// <summary>
    /// A helper to create the application tile
    /// </summary>
    public class ApplicationTileManager
    {
        /// <summary>
        /// Create the application tile by calling the webservice
        /// </summary>
        public async Task UpdateAsync()
        {
            IReadableLimitable<News> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<News>>();

            // Get last news Id
            int idLastNews = await model.GetLastInsertedId();

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            // Get last news Id saved in local storage
            int idLastNewsSaved = settings.Contains(LibResources.NewsStorageKey) ? (int)settings[LibResources.NewsStorageKey] : 0;

            // Compare the 2 codes. If they are differents, update the tile
            if (idLastNews != idLastNewsSaved)
            {
                News lastNews = await model.GetAsync(idLastNews);

                // The application tile is the first active tile, even if it's not pinned
                ShellTile existingTile = ShellTile.ActiveTiles.First();

                ShellTileData newTile = new FlipTileData
                {
                    BackgroundImage = new Uri(lastNews.ImageUrl, UriKind.Absolute),
                    BackContent = lastNews.Title,
                    BackTitle = lastNews.DateTime.ToString("g")
                };

                // Update the tile
                existingTile.Update(newTile);

                settings[LibResources.NewsStorageKey] = idLastNews;
                settings.Save();
            }
        }
    }
}