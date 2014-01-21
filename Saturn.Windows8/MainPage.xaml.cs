using EPSILab.SolarSystem.Saturn.ViewModel.Helpers;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Helpers;
using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Tiles;
using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Toasts;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// Home page
    /// </summary>
    public sealed partial class MainPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            //// Update the application tile
            //ApplicationTileManager.Create();

            //// Check is new elements have been published and displays a toast notification
            //ConferenceToastManager.CheckAndDisplay();
            //NewsToastManager.CheckAndDisplay();
            //ShowToastManager.CheckAndDisplay();
        }

        #endregion

        #region Page Events

        /// <summary>
        /// Raised when the user loads the page.
        /// Register to the MVVM Light Messenger.
        /// Load the menu items.
        /// Register the background task to the system.
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Register to the MVVM Light Messenger
            Messenger.Default.Register<VisualGenericGroup>(this, GoToMasterPage);
            Messenger.Default.Register<VisualGenericItem>(this, GoToDetailsPage);

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<Uri>(this, GoToWebPage);
            Messenger.Default.Register<object>(this, GoToAboutPage);

            // Load items
            if (e.NavigationMode == NavigationMode.New)
            {
                IMainViewModel viewModel = (IMainViewModel)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadMenuCommand.CanExecute(this))
                {
                    viewModel.LoadMenuCommand.Execute(this);
                }

                // Register the background tast to the system
                await BackgroundTaskRegistrationHelper.RegisterAsync();
            }
        }

        /// <summary>
        /// Raised when the user leaves the page.
        /// Unregister from the MVVM Light Messenger.
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);
            base.OnNavigatedFrom(e);
        }

        #endregion

        #region Controls Events

        /// <summary>
        /// Raised when the user changes the selected item.
        /// Check if the item is a seletable item and open the app bar if it's the case
        /// </summary>
        /// <param name="sender">GridView or ListView</param>
        /// <param name="e">Event arguments</param>
        private void ItemView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                VisualGenericItem selectedItem = (VisualGenericItem)e.AddedItems[0];

                // If the item is selectable, open the app bar
                IList<string> types = new List<string> { "News", "Conference", "Salon" };

                if (types.Contains(selectedItem.Type))
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

        #region Messenger Methods

        /// <summary>
        /// Open the selected item in the related master page
        /// </summary>
        /// <param name="group">Group and group items to display</param>
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

        /// <summary>
        /// Open the selected item in the related details page
        /// </summary>
        /// <param name="item">Item to display</param>
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

        /// <summary>
        /// Pin the selected item
        /// </summary>
        /// <param name="element">Element converted in a generic item</param>
        private void Pin(PinnableObject element)
        {
            PinHelper.Pin(element);
        }

        /// <summary>
        /// Open a social link (Website, Facebook page or Twitter page) in the browser
        /// </summary>
        /// <param name="uri">Social page URL</param>
        private async void GoToWebPage(Uri uri)
        {
            await Launcher.LaunchUriAsync(uri);
        }

        /// <summary>
        /// Opens the about page
        /// </summary>
        private void GoToAboutPage(object element)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        #endregion
    }
}