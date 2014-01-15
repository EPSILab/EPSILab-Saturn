using Autofac;
using Microsoft.Phone.Shell;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Resources;
using SolarSystem.Saturn.ViewModel;
using System;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

namespace SolarSystem.Saturn.View.WindowsPhone.TileFactory.Toasts
{
    /// <summary>
    /// Display a toast notification which warn the user a new conference will occur
    /// </summary>
    public static class ConferenceToastManager
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public static async Task CheckAndDisplay()
        {
            // Resolve model
            IReadableLimitable<Conference> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Conference>>();

            // Get last conference Id from model
            int idLastConference = await model.GetLastInsertedId();

            // Get last conference saved Id
            int idLastConferenceSaved = IsolatedStorageSettings.ApplicationSettings.Contains(LibResources.ConferenceStorageKey) ? (int)IsolatedStorageSettings.ApplicationSettings[LibResources.ConferenceStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastConference != idLastConferenceSaved)
            {
                Conference lastConference = await model.GetAsync(idLastConference);

                ShellToast toast = new ShellToast
                {
                    Title = LibResources.NewConference,
                    Content = lastConference.Nom,
                    NavigationUri = new Uri(string.Format("/ConferencePage.xaml?Id={0}", lastConference.Code_Conference), UriKind.Relative)
                };

                toast.Show();

                IsolatedStorageSettings.ApplicationSettings[LibResources.ConferenceStorageKey] = idLastConference;
            }
        }
    }
}