using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.ViewModel.Helpers;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.Win8.Helpers;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        #region Page Events

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Default.Register<VisualGenericGroup>(this, GoToMasterPage);
            Messenger.Default.Register<VisualGenericItem>(this, GoToDetailsPage);

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<Uri>(this, GoToSocialPage);
            Messenger.Default.Register<object>(this, GoToAboutPage);

            if (e.NavigationMode == NavigationMode.New)
            {
                IMainViewModel viewModel = (IMainViewModel)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadMenuCommand.CanExecute(this))
                {
                    viewModel.LoadMenuCommand.Execute(this);
                }

                await BackgroundTaskRegistrationHelper.RegisterAsync();
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnNavigatedFrom(e);
        }

        #endregion

        #region Controls's events handlers

        private void ItemView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                var itemSelectionne = (VisualGenericItem)e.AddedItems[0];

                if (itemSelectionne.Type == "News" || itemSelectionne.Type == "Conference" || itemSelectionne.Type == "Salon")
                {
                    AppBar.IsOpen = true;
                }
                else
                {
                    ((Selector)sender).SelectedItem = null;
                }
            }
            else
            {
                AppBar.IsOpen = false;
            }
        }

        #endregion

        #region Messenger methods

        private void GoToMasterPage(VisualGenericGroup group)
        {
            IDictionary<string, Func<Type>> pages = new Dictionary<string, Func<Type>>
                {
                    { AppResourcesHelper.GetString("LBL_NEWS"), () => typeof(NewsPage) },
                    { AppResourcesHelper.GetString("LBL_BUREAU"), () => typeof(MembresPage) },
                    { AppResourcesHelper.GetString("LBL_PROJECTS"), () => typeof(ProjetsPage) },
                    { AppResourcesHelper.GetString("LBL_CONFERENCES"), () => typeof(ConferencesPage) },
                    { AppResourcesHelper.GetString("LBL_SALONS"), () => typeof(SalonsPage) }
                };

            Type type = pages[group.Title]();
            Frame.Navigate(type);
        }

        private void GoToDetailsPage(VisualGenericItem item)
        {
            IDictionary<string, Func<Type>> pages = new Dictionary<string, Func<Type>>
                {
                    { "News", () => typeof(NewsDetailsPage) },
                    { "Membre", () => typeof(MembreDetailsPage) },
                    { "Projet", () => typeof(ProjetDetailsPage) },
                    { "Conference", () => typeof(ConferenceDetailsPage) },
                    { "Salon", () => typeof(SalonDetailsPage) }
                };

            Type type = pages[item.Type]();
            Frame.Navigate(type, item);
        }

        private async void Pin(PinnableObject element)
        {
            await PinTaskHelper.CreateTile(element);
        }

        private async void GoToSocialPage(Uri uri)
        {
            await Launcher.LaunchUriAsync(uri);
        }

        private void GoToAboutPage(object element)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        #endregion
    }
}