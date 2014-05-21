using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Formatters;
using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Resources;
using NotificationsExtensions.TileContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Tiles
{
    /// <summary>
    /// A class to create the application tile
    /// </summary>
    public class ApplicationTileManager
    {
        #region Attributes

        /// <summary>
        /// Number of items in lists
        /// </summary>
        private const int ItemsNumber = 5;

        #endregion

        #region Methods

        /// <summary>
        /// Create the application tile
        /// </summary>
        public async Task CreateAsync()
        {
            TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);

            // Clear tile notifications
            updater.Clear();

            // Add new notifications
            updater.Update(await AddConferencesToTileAsync());
            updater.Update(await AddNewsToTileAsync());
            updater.Update(await AddShowsToTileAsync());
            updater.Update(await AddNewsToTileAsync());
        }

        /// <summary>
        /// Add 5 last news on the tile
        /// </summary>
        /// <returns>The notification for the tile</returns>
        private async Task<TileNotification> AddNewsToTileAsync()
        {
            // Resolve the model
            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
            {
                // Get news from model
                IReadableLimitable<News> model = scope.Resolve<IReadableLimitable<News>>();
                IList<News> newsList = await model.GetAsync(0, ItemsNumber);

                // Create the tile
                if (newsList.Count == ItemsNumber)
                {
                    var random = new Random();
                    int index = random.Next(0, ItemsNumber);
                    News news = newsList[index];

                    // Create the square tile
                    ITileSquarePeekImageAndText02 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText02();

                    // Square tile text
                    squareContent.TextHeading.Text = DateFormatter.Format(news.DateTime);
                    squareContent.TextBodyWrap.Text = news.Title;

                    // Square tile image
                    squareContent.Image.Src = news.ImageUrl;
                    squareContent.Image.Alt = news.Title;

                    // Create the wide tile
                    ITileWideSmallImageAndText04 wideContent = TileContentFactory.CreateTileWideSmallImageAndText04();

                    // Link the square tile and the wide tile
                    wideContent.SquareContent = squareContent;

                    // Wide tile text
                    wideContent.TextHeading.Text = DateFormatter.Format(news.DateTime);
                    wideContent.TextBodyWrap.Text = news.Title;

                    // Wide tile image
                    wideContent.Image.Src = news.ImageUrl;
                    wideContent.Image.Alt = news.Title;

                    return wideContent.CreateNotification();
                }
            }

            return null;
        }

        /// <summary>
        /// Add 5 last conferences on the tile
        /// </summary>
        /// <returns>The notification for the tile</returns>
        private async Task<TileNotification> AddConferencesToTileAsync()
        {
            // Resolve the model
            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
            {
                // Get conferences from model
                IReadableLimitable<Conference> model = scope.Resolve<IReadableLimitable<Conference>>();
                IList<Conference> conferences = await model.GetAsync(0, ItemsNumber);

                // Create the square tile if there is one conference available
                if (conferences.Any())
                {
                    ITileSquarePeekImageAndText02 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText02();
                    squareContent.TextHeading.Text = ResourcesAccessor.GetString("Conferences_Small");
                    squareContent.TextBodyWrap.Text = conferences[0].Name;
                    squareContent.Image.Src = conferences[0].ImageUrl;
                    squareContent.Image.Alt = conferences[0].Name;

                    // Create the wide tile if there are enough elements
                    if (conferences.Count == ItemsNumber)
                    {
                        ITileWidePeekImageCollection02 wideContent = TileContentFactory.CreateTileWidePeekImageCollection02();

                        // Link the square tile and the wide tile
                        wideContent.SquareContent = squareContent;

                        // Texts
                        wideContent.TextHeading.Text = ResourcesAccessor.GetString("Conferences_Large");
                        wideContent.TextBody1.Text = conferences[0].Name;
                        wideContent.TextBody2.Text = conferences[1].Name;
                        wideContent.TextBody3.Text = conferences[2].Name;
                        wideContent.TextBody4.Text = conferences[3].Name;

                        // ImageUrls
                        wideContent.ImageMain.Src = conferences[0].ImageUrl;
                        wideContent.ImageMain.Alt = conferences[0].Name;

                        wideContent.ImageSmallColumn1Row1.Alt = conferences[1].Name;
                        wideContent.ImageSmallColumn1Row1.Src = conferences[1].ImageUrl;

                        wideContent.ImageSmallColumn1Row2.Alt = conferences[2].Name;
                        wideContent.ImageSmallColumn1Row2.Src = conferences[2].ImageUrl;

                        wideContent.ImageSmallColumn2Row1.Alt = conferences[3].Name;
                        wideContent.ImageSmallColumn2Row1.Src = conferences[3].ImageUrl;

                        wideContent.ImageSmallColumn2Row2.Alt = conferences[4].Name;
                        wideContent.ImageSmallColumn2Row2.Src = conferences[4].ImageUrl;

                        return wideContent.CreateNotification();
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Add 5 last shows on the tile
        /// </summary>
        /// <returns>The notification for the tile</returns>
        private async Task<TileNotification> AddShowsToTileAsync()
        {
            // Resolve model
            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
            {
                // Get the shows from the model
                IReadableLimitable<Show> model = scope.Resolve<IReadableLimitable<Show>>();
                IList<Show> shows = await model.GetAsync(0, ItemsNumber);

                // Create the square tile if there is one show available
                if (shows.Any())
                {
                    ITileSquarePeekImageAndText02 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText02();
                    squareContent.TextHeading.Text = ResourcesAccessor.GetString("Shows_Small");
                    squareContent.TextBodyWrap.Text = shows[0].Name;
                    squareContent.Image.Src = shows[0].ImageUrl;
                    squareContent.Image.Alt = shows[0].Name;

                    // Create the wide tile if there are enough elements
                    if (shows.Count == ItemsNumber)
                    {
                        ITileWidePeekImageCollection05 wideContent = TileContentFactory.CreateTileWidePeekImageCollection05();

                        // Link the square tile and the wide tile
                        wideContent.SquareContent = squareContent;

                        // Text
                        wideContent.TextHeading.Text = ResourcesAccessor.GetString("Shows_Long");
                        wideContent.TextBodyWrap.Text = string.Format("{0} ({1})", shows[0].Name, DateFormatter.Format(shows[0].Start_DateTime));

                        // ImageUrls
                        wideContent.ImageMain.Src = shows[0].ImageUrl;
                        wideContent.ImageMain.Alt = shows[0].Name;

                        wideContent.ImageSecondary.Src = shows[0].ImageUrl;
                        wideContent.ImageSecondary.Alt = shows[0].Name;

                        wideContent.ImageSmallColumn1Row1.Alt = shows[1].Name;
                        wideContent.ImageSmallColumn1Row1.Src = shows[1].ImageUrl;

                        wideContent.ImageSmallColumn1Row2.Alt = shows[2].Name;
                        wideContent.ImageSmallColumn1Row2.Src = shows[2].ImageUrl;

                        wideContent.ImageSmallColumn2Row1.Alt = shows[3].Name;
                        wideContent.ImageSmallColumn2Row1.Src = shows[3].ImageUrl;

                        wideContent.ImageSmallColumn2Row2.Alt = shows[4].Name;
                        wideContent.ImageSmallColumn2Row2.Src = shows[4].ImageUrl;

                        return wideContent.CreateNotification();
                    }
                }
            }

            return null;
        }

        #endregion
    }
}