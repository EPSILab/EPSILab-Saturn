using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Factories;
using EPSILab.SolarSystem.Saturn.Windows8.Helpers;
using GalaSoft.MvvmLight.Messaging;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// Shows page
    /// </summary>
    public sealed partial class SalonsPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SalonsPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Determines if the collection is loaded. Prevent to select the first item on the first list loading
        /// </summary>
        private bool _isCollectionLoaded;

        /// <summary>
        /// Share contract
        /// </summary>
        private ShareContractFactory _shareContractFactory;

        #endregion

        #region Messenger methods

        /// <summary>
        /// Open the show in the details page
        /// </summary>
        /// <param name="element">Show to display</param>
        private void GoToDetailsPage(Salon element)
        {
            Frame.Navigate(typeof(SalonDetailsPage), element);
        }

        /// <summary>
        /// Pin the selected show
        /// </summary>
        /// <param name="element">Element to pin</param>
        private void Pin(PinnableObject element)
        {
            PinHelper.Pin(element);
        }

        /// <summary>
        /// Share the show in another app.
        /// Raise the "SalonPage_DataRequested" event.
        /// </summary>
        /// <param name="element">Element to share</param>
        private void Share(ShareableObject element)
        {
            try
            {
                DataTransferManager.GetForCurrentView().DataRequested -= SalonPage_DataRequested;
            }
            finally
            {
                _shareContractFactory = new ShareContractFactory((ShareableWin8Object)element);
                DataTransferManager.GetForCurrentView().DataRequested += SalonPage_DataRequested;
            }

            DataTransferManager.ShowShareUI();
        }

        #endregion

        #region Page events

        /// <summary>
        /// Raised when the user load page
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Register to the MVVM Light Toolkit Messenger
            Messenger.Default.Register<Salon>(this, GoToDetailsPage);
            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);

            // If the user loads the page for the first time, load shows from the model
            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<Salon> viewModel = (IMasterViewModel<Salon>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        /// <summary>
        /// Raised when the user leaves page
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);

            if (e.NavigationMode == NavigationMode.Back)
                ViewModelLocator.DisposeMasterVM<Salon>();
            else
                ViewModelLocator.CleanMasterVM<Salon>();

            base.OnNavigatedFrom(e);
        }

        /// <summary>
        /// Raised when the user wants to share a show in another app
        /// </summary>
        /// <param name="sender">Element which raised the event</param>
        /// <param name="args">Event arguments</param>
        private void SalonPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            _shareContractFactory.DisplayShareUI(args);
        }

        #endregion

        #region Controls events

        /// <summary>
        /// Raised when the user changes the selected item
        /// </summary>
        /// <param name="sender">ListView (snapped view) or GridView (others view)</param>
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
                    else
                    {
                        AppBar.IsOpen = e.AddedItems.Count == 1;
                    }
                }
            }
        }

        /// <summary>
        /// Raised when the user clicks on the Refresh button. Tells the collection is not loaded anymore
        /// </summary>
        /// <param name="sender">Refresh button</param>
        /// <param name="e">Event args</param>
        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            _isCollectionLoaded = false;
        }

        #endregion
    }
}