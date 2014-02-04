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
    /// Display a toast notification which warn the user a new event will occur
    /// </summary>
    public class SalonToastManager : ToastManager
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public override async Task CheckAndToastAsync()
        {
            // Resolve model
            IReadableLimitable<Salon> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Salon>>();

            // Get last conference Id from model
            int idLastSalon = await model.GetLastInsertedId();

            IsolatedStorageSettings localSettings = IsolatedStorageSettings.ApplicationSettings;

            // Get last conference saved Id
            int idLastSalonSaved = localSettings.Contains(LibResources.SalonStorageKey) ? (int)localSettings[LibResources.SalonStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastSalon != idLastSalonSaved)
            {
                Salon lastSalon = await model.GetAsync(idLastSalon);

                ShellToast toast = new ShellToast
                {
                    Title = LibResources.NewSalon,
                    Content =  lastSalon.Nom,
                    NavigationUri = new Uri(string.Format("/SalonPage.xaml?Id={0}", lastSalon.Code_Salon), UriKind.Relative)
                };

                toast.Show();

                localSettings[LibResources.SalonStorageKey] = idLastSalon;
                localSettings.Save();
            }
        }
    }
}