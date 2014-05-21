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
    public class ShowToastManager : ToastManager
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        public override async Task CheckAndToastAsync()
        {
            // Resolve model
            IReadableLimitable<Show> model;

            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
                model = scope.Resolve<IReadableLimitable<Show>>();

            // Get last conference Id from model
            int idLastShow = await model.GetLastInsertedId();

            IsolatedStorageSettings localSettings = IsolatedStorageSettings.ApplicationSettings;

            // Get last conference saved Id
            int idLastShowSaved = localSettings.Contains(LibResources.ShowStorageKey) ? (int)localSettings[LibResources.ShowStorageKey] : 0;

            // If Ids are differents, update the saved Id and show a toast notification
            if (idLastShow != idLastShowSaved)
            {
                Show lastShow = await model.GetAsync(idLastShow);

                ShellToast toast = new ShellToast
                {
                    Title = LibResources.NewShow,
                    Content =  lastShow.Name,
                    NavigationUri = new Uri(string.Format("/ShowPage.xaml?Id={0}", lastShow.Id), UriKind.Relative)
                };

                toast.Show();

                localSettings[LibResources.ShowStorageKey] = idLastShow;
                localSettings.Save();
            }
        }
    }
}