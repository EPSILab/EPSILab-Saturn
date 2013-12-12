using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using SolarSystem.Saturn.DataAccess;
using SolarSystem.Saturn.DataAccess.Interfaces;
using SolarSystem.Saturn.DataAccess.Webservice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace SolarSystem.Saturn.WP8.SheduledTaskAgent
{
    public class CreateApplicationTileAgent : ScheduledTaskAgent
    {
        static CreateApplicationTileAgent()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => Application.Current.UnhandledException += UnhandledException);
        }

        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        protected override async void OnInvoke(ScheduledTask task)
        {
            try
            {
                // Load last news
                IReadableLimitable<News> dal = new NewsDAL();

                IList<News> listNews = await dal.GetAsync(0, 1);
                News lastNews = listNews.First();

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
            }
            finally
            {
                NotifyComplete();
            }
        }
    }
}