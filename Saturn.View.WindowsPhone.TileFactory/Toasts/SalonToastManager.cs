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
    /// Display a toast notification which warn the user a new event will occur
    /// </summary>
    public static class DisplayLastShowToast
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public static async Task CheckAndDisplay()
        {
            // Resolve model
            IReadableLimitable<Salon> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Salon>>();

            // Get last conference Id from model
            int idLastSalon = await model.GetLastInsertedId();

            // Get last conference saved Id
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            int idLastSalonSaved = settings.Contains(LibResources.SalonStorageKey) ? (int)settings[LibResources.SalonStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastSalon != idLastSalonSaved)
            {
                Salon lastSalon = await model.GetAsync(idLastSalon);

                string message = string.Format("{0} - {1} @ {2}", lastSalon.Nom, lastSalon.Date_Heure_Debut, lastSalon.Lieu);
                DisplayToastHelper.Display(message);

                settings[LibResources.SalonStorageKey] = idLastSalon;
            }
        }
    }
}