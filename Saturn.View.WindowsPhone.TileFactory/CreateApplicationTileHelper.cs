using Microsoft.Phone.Shell;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.View.WindowsPhone.TileFactory
{
    /// <summary>
    /// A helper to create the application tile
    /// </summary>
    public static class CreateApplicationTileHelper
    {
        /// <summary>
        /// Create the application tile with the passed argument
        /// </summary>
        /// <param name="news">The news with which to create the tile</param>
        /// <returns></returns>
        public static void Create(News news)
        {
            // The application tile is the first active tile, even if it's not pinned
            ShellTile existingTile = ShellTile.ActiveTiles.First();

            ShellTileData newTile = new FlipTileData
            {
                BackgroundImage = new Uri(news.Image, UriKind.Absolute),
                BackContent = news.Titre,
                BackTitle = news.Date_Heure.ToString("g")
            };

            // Update the tile
            existingTile.Update(newTile);
        }

        /// <summary>
        /// Create the application tile by calling the webservice
        /// </summary>
        /// <param name="model">Access to the model</param>
        public static async Task Create(IReadableLimitable<News> model)
        {
            IList<News> listNews = await model.GetAsync(0, 1);
            News lastNews = listNews.First();

            Create(lastNews);
        }
    }
}