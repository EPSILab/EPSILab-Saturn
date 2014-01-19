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
    /// Conference master page
    /// </summary>
    public sealed partial class ConferencesPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConferencesPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Prevents to select the first item when the collection is loaded
        /// </summary>
        private bool _isCollectionLoaded;

        /// <summary>
        /// A helper to share the selected conference to others apps
        /// </summary>
        private ShareContractFactory _shareContractFactory;

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

            // Register to the MVVM Light Messenger
            Messenger.Default.Register<Conference>(this, GoToDetailsPage);
            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);

            // If the user loads the page for the first time, load elements
            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<Conference> viewModel = (IMasterViewModel<Conference>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        /// <summary>
        /// Raised when the user leaves the page.
        /// Unregister from the MVVM Light Messenger
        /// Clean the associated view-model if the user goes back to the Main page or Master page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Unregister to the MVVM Light Messenger
            Messenger.Default.Unregister(this);

            if (e.NavigationMode == NavigationMode.Back)
                ViewModelLocator.DisposeMasterVM<Conference>();
            else
                ViewModelLocator.CleanMasterVM<Conference>();

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Raised when the user wants to share the conference
        /// </summary>
        /// <param name="sender">Programmatically initiates an exchange of content with other apps.</param>
        /// <param name="args">Share event arguments</param>
        private void ConferencesPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            _shareContractFactory.DisplayShareUI(args);
        }

        #endregion

        #region Controls Events

        /// <summary>
        /// Raised when the user changes the selected item
        /// </summary>
        /// <param name="sender">GridView or ListView</param>
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
                    else if(AppBar != null)
                    {
                        AppBar.IsOpen = e.AddedItems.Count == 1;
                    }
                }
            }
        }

        /// <summary>
        /// Raised when the user clicks on the Refresh button
        /// </summary>
        /// <param name="sender">Refresh button</param>
        /// <param name="e">Event arguments</param>
        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            _isCollectionLoaded = false;
        }

        #endregion

        #region Messenger Methods

        /// <summary>
        /// Show the conference in the details page
        /// </summary>
        /// <param name="conference">Conference to display</param>
        private void GoToDetailsPage(Conference conference)
        {
            Frame.Navigate(typeof(ConferenceDetailsPage), conference);
        }

        /// <summary>
        /// Pin the selected conference
        /// </summary>
        /// <param name="element">Conference converted in a generic object to pin</param>
        private void Pin(PinnableObject element)
        {
            PinHelper.Pin(element);
        }

        /// <summary>
        /// Display the Share UI 
        /// </summary>
        /// <param name="conference">Conference converted in a generic object to share</param>
        private void Share(ShareableObject conference)
        {
            try
            {
                DataTransferManager.GetForCurrentView().DataRequested -= ConferencesPage_DataRequested;
            }
            finally
            {
                _shareContractFactory = new ShareContractFactory((ShareableWin8Object)conference);
                DataTransferManager.GetForCurrentView().DataRequested += ConferencesPage_DataRequested;
            }

            DataTransferManager.ShowShareUI();
        }

        #endregion
    }
}