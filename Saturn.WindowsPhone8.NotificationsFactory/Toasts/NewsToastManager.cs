﻿using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.TileFactory.Resources;
using Microsoft.Phone.Shell;
using System;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8.TileFactory.Toasts
{
    /// <summary>
    /// Display a toast notification when a news has been published
    /// </summary>
    public class NewsToastManager : ToastManager
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public override async Task CheckAndToastAsync()
        {
            // Resolve model
            IReadableLimitable<News> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<News>>();

            // Get last news Id from model
            int idLastNews = await model.GetLastInsertedId();

            IsolatedStorageSettings localSettings = IsolatedStorageSettings.ApplicationSettings;

            // Get last news saved Id
            int idLastNewsSaved = localSettings.Contains(LibResources.NewsStorageKey) ? (int)localSettings[LibResources.NewsStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastNews != idLastNewsSaved)
            {
                News lastNews = await model.GetAsync(idLastNews);

                ShellToast toast = new ShellToast
                {
                    Title = LibResources.NewNews,
                    Content = lastNews.Title,
                    NavigationUri = new Uri(string.Format("/NewsPage.xaml?Id={0}", lastNews.Id), UriKind.Relative)
                };

                toast.Show();

                localSettings[LibResources.NewsStorageKey] = idLastNews;
                localSettings.Save();
            }
        }
    }
}