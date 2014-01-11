using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.Win8.Helpers;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Search;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class SearchPage
    {
        public SearchPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        #region Page events

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            SearchPane.GetForCurrentView().QuerySubmitted += RechercherPage_QuerySubmitted;

            if (navigationParameter != null)
            {
                Search(navigationParameter.ToString());
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<VisualGenericItem>(this, GoToDetailsPage);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Messenger.Default.Unregister(this);
            ViewModelLocator.CleanSearchVM(false);
        }

        private void RechercherPage_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            Search(args.QueryText);
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            SearchPane.GetForCurrentView().QuerySubmitted -= RechercherPage_QuerySubmitted;

            ViewModelLocator.CleanSearchVM(true);
            base.GoBack(sender, e);
        }

        #endregion

        #region Messenger methods

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

        private void Pin(PinnableObject element)
        {
            PinHelper.Pin(element);
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

        #region Methods

        private void Search(string keyword)
        {
            ISearchViewModel viewModel = (ISearchViewModel)DataContext;

            if (viewModel.SearchCommand.CanExecute(keyword))
            {
                viewModel.SearchCommand.Execute(keyword);
            }
        }

        #endregion
    }
}