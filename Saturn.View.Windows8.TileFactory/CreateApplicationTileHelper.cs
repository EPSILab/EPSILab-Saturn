using Autofac;
using NotificationsExtensions.TileContent;
using Saturn.View.Windows8.TileFactory.Resources;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Saturn.View.Windows8.TileFactory
{
    public static class CreateApplicationTileHelper
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
        public static async void CreateAsync()
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
        private static async Task<TileNotification> AddNewsToTileAsync()
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
                    squareContent.TextHeading.Text = DateFormatter.Format(news.Date_Heure);
                    squareContent.TextBodyWrap.Text = news.Titre;

                    // Square tile image
                    squareContent.Image.Src = news.Image;
                    squareContent.Image.Alt = news.Titre;

                    // Create the wide tile
                    ITileWideSmallImageAndText04 wideContent = TileContentFactory.CreateTileWideSmallImageAndText04();

                    // Link the square tile and the wide tile
                    wideContent.SquareContent = squareContent;

                    // Wide tile text
                    wideContent.TextHeading.Text = DateFormatter.Format(news.Date_Heure);
                    wideContent.TextBodyWrap.Text = news.Titre;

                    // Wide tile image
                    wideContent.Image.Src = news.Image;
                    wideContent.Image.Alt = news.Titre;

                    return wideContent.CreateNotification();
                }
            }

            return null;
        }

        /// <summary>
        /// Add 5 last conferences on the tile
        /// </summary>
        /// <returns>The notification for the tile</returns>
        private static async Task<TileNotification> AddConferencesToTileAsync()
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
                    squareContent.TextHeading.Text = ResourcesRsxAccessor.GetString("Conferences_Short");
                    squareContent.TextBodyWrap.Text = conferences[0].Nom;
                    squareContent.Image.Src = conferences[0].Image;
                    squareContent.Image.Alt = conferences[0].Nom;

                    // Create the wide tile if there are enough elements
                    if (conferences.Count == ItemsNumber)
                    {
                        ITileWidePeekImageCollection02 wideContent = TileContentFactory.CreateTileWidePeekImageCollection02();

                        // Link the square tile and the wide tile
                        wideContent.SquareContent = squareContent;

                        // Texts
                        wideContent.TextHeading.Text = ResourcesRsxAccessor.GetString("Conferences_Large");
                        wideContent.TextBody1.Text = conferences[0].Nom;
                        wideContent.TextBody2.Text = conferences[1].Nom;
                        wideContent.TextBody3.Text = conferences[2].Nom;
                        wideContent.TextBody4.Text = conferences[3].Nom;

                        // Images
                        wideContent.ImageMain.Src = conferences[0].Image;
                        wideContent.ImageMain.Alt = conferences[0].Nom;

                        wideContent.ImageSmallColumn1Row1.Alt = conferences[1].Nom;
                        wideContent.ImageSmallColumn1Row1.Src = conferences[1].Image;

                        wideContent.ImageSmallColumn1Row2.Alt = conferences[2].Nom;
                        wideContent.ImageSmallColumn1Row2.Src = conferences[2].Image;

                        wideContent.ImageSmallColumn2Row1.Alt = conferences[3].Nom;
                        wideContent.ImageSmallColumn2Row1.Src = conferences[3].Image;

                        wideContent.ImageSmallColumn2Row2.Alt = conferences[4].Nom;
                        wideContent.ImageSmallColumn2Row2.Src = conferences[4].Image;

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
        private static async Task<TileNotification> AddShowsToTileAsync()
        {
            // Resolve model
            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
            {
                // Get the shows from the model
                IReadableLimitable<Salon> model = scope.Resolve<IReadableLimitable<Salon>>();
                IList<Salon> shows = await model.GetAsync(0, ItemsNumber);

                // Create the square tile if there is one show available
                if (shows.Any())
                {
                    ITileSquarePeekImageAndText02 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText02();
                    squareContent.TextHeading.Text = ResourcesRsxAccessor.GetString("Shows_Short");
                    squareContent.TextBodyWrap.Text = shows[0].Nom;
                    squareContent.Image.Src = shows[0].Image;
                    squareContent.Image.Alt = shows[0].Nom;

                    // Create the wide tile if there are enough elements
                    if (shows.Count == ItemsNumber)
                    {
                        ITileWidePeekImageCollection05 wideContent = TileContentFactory.CreateTileWidePeekImageCollection05();

                        // Link the square tile and the wide tile
                        wideContent.SquareContent = squareContent;

                        // Text
                        wideContent.TextHeading.Text = ResourcesRsxAccessor.GetString("Shows_Long");
                        wideContent.TextBodyWrap.Text = string.Format("{0} ({1})", shows[0].Nom, DateFormatter.Format(shows[0].Date_Heure_Debut));

                        // Images
                        wideContent.ImageMain.Src = shows[0].Image;
                        wideContent.ImageMain.Alt = shows[0].Nom;

                        wideContent.ImageSecondary.Src = shows[0].Image;
                        wideContent.ImageSecondary.Alt = shows[0].Nom;

                        wideContent.ImageSmallColumn1Row1.Alt = shows[1].Nom;
                        wideContent.ImageSmallColumn1Row1.Src = shows[1].Image;

                        wideContent.ImageSmallColumn1Row2.Alt = shows[2].Nom;
                        wideContent.ImageSmallColumn1Row2.Src = shows[2].Image;

                        wideContent.ImageSmallColumn2Row1.Alt = shows[3].Nom;
                        wideContent.ImageSmallColumn2Row1.Src = shows[3].Image;

                        wideContent.ImageSmallColumn2Row2.Alt = shows[4].Nom;
                        wideContent.ImageSmallColumn2Row2.Src = shows[4].Image;

                        return wideContent.CreateNotification();
                    }
                }
            }

            return null;
        }

        #endregion
    }
}