using Autofac;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Helpers;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Resources;
using SolarSystem.Saturn.ViewModel;
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
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            int idLastConferenceSaved = settings.Contains(LibResources.ConferenceStorageKey) ? (int)settings[LibResources.ConferenceStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastConference != idLastConferenceSaved)
            {
                Conference lastConference = await model.GetAsync(idLastConference);

                string message = string.Format("{0} - {1} @ {2}", lastConference.Nom, lastConference.Date_Heure_Debut, lastConference.Lieu);
                DisplayToastHelper.Display(message);

                settings[LibResources.ConferenceStorageKey] = idLastConference;
            }
        }
    }
}