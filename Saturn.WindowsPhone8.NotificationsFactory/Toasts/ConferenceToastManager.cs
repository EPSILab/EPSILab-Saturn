using Autofac;
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
    /// Display a toast notification which warn the user a new conference will occur
    /// </summary>
    public class ConferenceToastManager : ToastManager
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public override async Task CheckAndToastAsync()
        {
            // Resolve model
            IReadableLimitable<Conference> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Conference>>();

            // Get last conference Id from model
            int idLastConference = await model.GetLastInsertedId();

            IsolatedStorageSettings localSettings = IsolatedStorageSettings.ApplicationSettings;

            // Get last conference saved Id
            int idLastConferenceSaved = localSettings.Contains(LibResources.ConferenceStorageKey) ? (int)localSettings[LibResources.ConferenceStorageKey] : 0;

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

                localSettings[LibResources.ConferenceStorageKey] = idLastConference;
                localSettings.Save();
            }
        }
    }
}