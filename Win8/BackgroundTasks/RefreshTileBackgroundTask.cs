using NotificationsExtensions.TileContent;
using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel.Formatters;
using SolarSystem.Saturn.Win8.BackgroundTasks.Resources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace SolarSystem.Saturn.Win8.BackgroundTasks
{
    public sealed class RefreshTileBackgroundTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            try
            {
                TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.EnableNotificationQueue(true);
                updater.Clear();

                updater.Update(await CreateConferencesTile());
                updater.Update(await CreateNewsTile());
                updater.Update(await CreateSalonsTile());
                updater.Update(await CreateNewsTile());
            }
            finally
            {
                // Informs the system task is finished
                deferral.Complete();
            }
        }

        private static async Task<TileNotification> CreateNewsTile()
        {
            IReadableLimitable<News> newsModel = new NewsDAL();
            IList<News> newsList = await newsModel.GetAsync(0, 3);

            if (newsList.Count >= 3)
            {
                var random = new Random();
                int index = random.Next(0, 3);
                News news = newsList[index];

                ITileSquarePeekImageAndText02 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText02();
                squareContent.TextHeading.Text = DateFormatter.Format(news.Date_Heure);
                squareContent.Image.Src = news.Image;
                squareContent.Image.Alt = news.Titre;
                squareContent.TextBodyWrap.Text = news.Titre;

                ITileWideSmallImageAndText04 wideContent = TileContentFactory.CreateTileWideSmallImageAndText04();
                wideContent.SquareContent = squareContent;

                wideContent.TextHeading.Text = DateFormatter.Format(news.Date_Heure);
                wideContent.TextBodyWrap.Text = news.Titre;
                wideContent.Image.Src = news.Image;
                wideContent.Image.Alt = news.Titre;

                return wideContent.CreateNotification();
            }

            return null;
        }

        private static async Task<TileNotification> CreateConferencesTile()
        {
            var conferenceModel = new ConferenceDAL();
            IList<Conference> conferences = await conferenceModel.GetAsync(0, 5);

            IList<int> indexes = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                int index = new Random().Next(conferences.Count);

                while (indexes.Contains(index))
                {
                    index = new Random().Next(conferences.Count);
                }

                indexes.Add(index);
            }

            if (conferences.Count >= indexes.Count)
            {
                ITileSquarePeekImageAndText02 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText02();
                squareContent.TextHeading.Text = ResourcesRsxAccessor.GetString("CONFERENCES_SMALL");
                squareContent.Image.Src = conferences[indexes[0]].Image;
                squareContent.Image.Alt = conferences[indexes[0]].Nom;
                squareContent.TextBodyWrap.Text = conferences[indexes[0]].Nom;

                ITileWidePeekImageCollection02 wideContent = TileContentFactory.CreateTileWidePeekImageCollection02();

                wideContent.SquareContent = squareContent;

                wideContent.TextBody1.Text = conferences[indexes[0]].Nom;
                wideContent.ImageMain.Src = conferences[indexes[0]].Image;
                wideContent.ImageMain.Alt = conferences[indexes[0]].Nom;

                wideContent.TextBody2.Text = conferences[indexes[1]].Nom;
                wideContent.ImageSmallColumn1Row1.Alt = conferences[indexes[1]].Nom;
                wideContent.ImageSmallColumn1Row1.Src = conferences[indexes[1]].Image;

                wideContent.TextBody3.Text = conferences[indexes[2]].Nom;
                wideContent.ImageSmallColumn1Row2.Alt = conferences[indexes[2]].Nom;
                wideContent.ImageSmallColumn1Row2.Src = conferences[indexes[2]].Image;

                wideContent.TextBody4.Text = conferences[indexes[3]].Nom;
                wideContent.ImageSmallColumn2Row1.Alt = conferences[indexes[3]].Nom;
                wideContent.ImageSmallColumn2Row1.Src = conferences[indexes[3]].Image;

                wideContent.ImageSmallColumn2Row2.Alt = conferences[indexes[4]].Nom;
                wideContent.ImageSmallColumn2Row2.Src = conferences[indexes[4]].Image;

                wideContent.TextHeading.Text = ResourcesRsxAccessor.GetString("CONFERENCES_LARGE");

                return wideContent.CreateNotification();
            }

            return null;
        }

        private static async Task<TileNotification> CreateSalonsTile()
        {
            var salonModel = new SalonDAL();
            IList<Salon> salons = await salonModel.GetAsync(0, 5);

            if (salons.Count >= 5)
            {
                ITileSquarePeekImageAndText02 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText02();
                squareContent.TextHeading.Text = ResourcesRsxAccessor.GetString("SHOWS_SMALL");
                squareContent.Image.Src = salons[0].Image;
                squareContent.Image.Alt = salons[0].Nom;
                squareContent.TextBodyWrap.Text = salons[0].Nom;

                ITileWidePeekImageCollection05 wideContent = TileContentFactory.CreateTileWidePeekImageCollection05();

                wideContent.SquareContent = squareContent;

                wideContent.ImageMain.Src = salons[0].Image;
                wideContent.ImageMain.Alt = salons[0].Nom;
                wideContent.ImageSmallColumn1Row1.Alt = salons[1].Nom;
                wideContent.ImageSmallColumn1Row1.Src = salons[1].Image;
                wideContent.ImageSmallColumn1Row2.Alt = salons[2].Nom;
                wideContent.ImageSmallColumn1Row2.Src = salons[2].Image;
                wideContent.ImageSmallColumn2Row1.Alt = salons[3].Nom;
                wideContent.ImageSmallColumn2Row1.Src = salons[3].Image;
                wideContent.ImageSmallColumn2Row2.Alt = salons[4].Nom;
                wideContent.ImageSmallColumn2Row2.Src = salons[4].Image;

                wideContent.TextHeading.Text = ResourcesRsxAccessor.GetString("SHOWS_LARGE");
                wideContent.TextBodyWrap.Text = string.Format("{0} ({1})", salons[0].Nom, DateFormatter.Format(salons[0].Date_Heure_Debut));
                wideContent.ImageSecondary.Src = salons[0].Image;
                wideContent.ImageSecondary.Alt = salons[0].Nom;

                return wideContent.CreateNotification();
            }

            return null;
        }
    }
}