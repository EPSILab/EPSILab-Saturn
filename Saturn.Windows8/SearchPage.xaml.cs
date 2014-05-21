using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Helpers;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Search;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// Search page
    /// </summary>
    public sealed partial class SearchPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SearchPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        #endregion

        #region Page Events

        /// <summary>
        /// Raised when the user loads or resumes the page
        /// Get search keyword if exists and launch search
        /// </summary>
        /// <param name="navigationParameter">Passed navigation parameters</param>
        /// <param name="pageState">Contains saved informations before the page was suspended</param>
        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            SearchPane.GetForCurrentView().QuerySubmitted += SearchPage_QuerySubmitted;

            if (navigationParameter != null && !string.IsNullOrWhiteSpace(navigationParameter.ToString()))
            {
                Search(navigationParameter.ToString());
            }
        }

        /// <summary>
        /// Raised when the user opens the page
        /// Register to MVVM Light Messenger
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<VisualGenericItem>(this, GoToDetailsPage);
        }

        /// <summary>
        /// Raised when the user leaves the page
        /// Unregister from the MVVM Light Messenger
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            SearchPane.GetForCurrentView().QuerySubmitted -= SearchPage_QuerySubmitted;

            Messenger.Default.Unregister(this);

            if (e.NavigationMode == NavigationMode.Back)
                ViewModelLocator.DisposeSearchVM();
            else
                ViewModelLocator.CleanSearchVM();
        }

        /// <summary>
        /// Raised when the user search in the app with the Charm bar
        /// </summary>
        /// <param name="sender">Search Pane</param>
        /// <param name="args">Contains search query</param>
        private void SearchPage_QuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            Search(args.QueryText);
        }

        #endregion

        #region Messenger methods

        /// <summary>
        /// Open the element in its adapted details page
        /// </summary>
        /// <param name="item"></param>
        private void GoToDetailsPage(VisualGenericItem item)
        {
            IDictionary<string, Func<Type>> pages = new Dictionary<string, Func<Type>>
                {
                    { "News", () => typeof(NewsDetailsPage) },
                    { "Member", () => typeof(MemberDetailsPage) },
                    { "Project", () => typeof(ProjectDetailsPage) },
                    { "Conference", () => typeof(ConferenceDetailsPage) },
                    { "Show", () => typeof(ShowDetailsPage) }
                };

            Type type = pages[item.Type]();
            Frame.Navigate(type, item);
        }

        /// <summary>
        /// Pin the selected item
        /// </summary>
        /// <param name="element">Element to pin converted in a generic object</param>
        private async void Pin(PinnableObject element)
        {
            CreateSecondaryTileHelper helper = new CreateSecondaryTileHelper();
            await helper.PinAsync(element);
        }

        #endregion

        #region Controls Events

        /// <summary>
        /// Raised when the user changes the selected item in the ListView or GridView
        /// </summary>
        /// <param name="sender">GridView or ListView</param>
        /// <param name="e">Contains selected and unselected item</param>
        private void ItemView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                VisualGenericItem selectedItem = (VisualGenericItem)e.AddedItems[0];

                IList<string> types = new List<string> { "News", "Conference", "Show" };

                if (types.Contains(selectedItem.Type))
                {
                    AppBar.IsOpen = true;
                }
                else
                {
                    Selector selector = (Selector)sender;
                    selector.SelectedItem = null;
                }
            }
            else
            {
                AppBar.IsOpen = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Launch the view-model's search command
        /// </summary>
        /// <param name="keyword">Search keyword</param>
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