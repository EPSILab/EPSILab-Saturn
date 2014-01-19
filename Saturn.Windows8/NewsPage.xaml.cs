using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Factories;
using EPSILab.SolarSystem.Saturn.Windows8.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// News master page
    /// </summary>
    public sealed partial class NewsPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NewsPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Attributes

        /// <summary>
        /// A boolean to prevent the selection of the first item when the collection is loaded
        /// </summary>
        private bool _isCollectionLoaded;

        /// <summary>
        /// Share contract factory in terms of the selected item
        /// </summary>
        private ShareContractFactory _shareContractFactory;

        #endregion

        #region Messenger methods

        /// <summary>
        /// Show the clicked item in to the details page
        /// </summary>
        /// <param name="news">News to display</param>
        private void GoToDetailsPage(News news)
        {
            Frame.Navigate(typeof(NewsDetailsPage), news);
        }

        /// <summary>
        /// Show the UI to pin the selected item
        /// </summary>
        /// <param name="news">Selected news</param>
        private void Pin(PinnableObject news)
        {
            PinHelper.Pin(news);
        }

        /// <summary>
        /// Modify the Share contract factory according to the selected news 
        /// </summary>
        /// <param name="news">Selected news</param>
        private void Share(ShareableObject news)
        {
            try
            {
                DataTransferManager.GetForCurrentView().DataRequested -= NewsPage_DataRequested;
            }
            finally
            {
                _shareContractFactory = new ShareContractFactory((ShareableWin8Object)news);
                DataTransferManager.GetForCurrentView().DataRequested += NewsPage_DataRequested;
            }

            DataTransferManager.ShowShareUI();
        }

        #endregion

        #region Page Events

        /// <summary>
        /// Raised when the user loads the page
        /// Register to the MVVM Light Messenger
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Register to MVVM Light Toolkit Messenger
            Messenger.Default.Register<News>(this, GoToDetailsPage);
            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);

            // If the user loads the page for the first time, load elements
            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<News> viewModel = (IMasterViewModel<News>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        /// <summary>
        /// Raised when the user goes leaves the page
        /// Unregister from the MVVM Light Messenger and clean the view-model 
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Unregister from the MVVM Light Messenger
            Messenger.Default.Unregister(this);

            // Clean the associated view-model
            if (e.NavigationMode == NavigationMode.Back)
                ViewModelLocator.DisposeMasterVM<News>();
            else
                ViewModelLocator.CleanMasterVM<News>();

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Raised the user wants to display the Windows 8 Share UI
        /// </summary>
        /// <param name="sender">Programmatically initiates an exchange of content with other apps.</param>
        /// <param name="args">Event arguments</param>
        private void NewsPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            _shareContractFactory.DisplayShareUI(args);
        }

        #endregion

        #region Controls Events

        /// <summary>
        /// Raised when the user change the selected item
        /// </summary>
        /// <param name="sender">ListView or GridView</param>
        /// <param name="e">Event arguments</param>
        private void ItemView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = (Selector)sender;

            if (selector.SelectedItem != null)
            {
                if (e.AddedItems.Count > 0)
                {
                    // _isCollectionLoaded allow to not select first item on page loading
                    if (!_isCollectionLoaded)
                    {
                        _isCollectionLoaded = true;

                        selector.SelectedItem = null;
                    }
                    else if (AppBar != null)
                    {
                        AppBar.IsOpen = e.AddedItems.Count == 1;
                    }
                }
            }
        }

        /// <summary>
        /// Raised when the user clicks on the Refresh button. Reset the collection state
        /// </summary>
        /// <param name="sender">Refresh button</param>
        /// <param name="e">Events arguments</param>
        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            _isCollectionLoaded = false;
        }

        #endregion
    }
}