using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.Win8.Factories;
using SolarSystem.Saturn.Win8.Helpers;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class ConferencesPage
    {
        #region Private fields

        private bool _isCollectionLoaded;
        private ShareContractFactory _shareContractFactory;

        #endregion

        public ConferencesPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        #region Page events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Default.Register<Conference>(this, GoToDetailsPage);
            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);

            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<Conference> viewModel = (IMasterViewModel<Conference>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);
            ViewModelLocator.CleanMasterVM<Conference>(false);

            base.OnNavigatedFrom(e);
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanMasterVM<Conference>(true);
            base.GoBack(sender, e);
        }

        private void ConferencesPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            _shareContractFactory.DisplayShareUI(args);
        }

        #endregion

        #region Messenger methods

        private void GoToDetailsPage(Conference conference)
        {
            Frame.Navigate(typeof(ConferenceDetailsPage), conference);
        }

        private void Pin(PinnableObject element)
        {
            PinHelper.Pin(element);
        }

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

        #region Controls's events handlers

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

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            _isCollectionLoaded = false;
        }

        #endregion
    }
}