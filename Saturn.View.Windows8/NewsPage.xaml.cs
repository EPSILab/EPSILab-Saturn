using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.DataAccess.Webservice;
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
    public sealed partial class NewsPage
    {
        #region Attributes

        private bool _isCollectionLoaded;
        private ShareContractFactory _shareContractFactory;

        #endregion

        public NewsPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        #region Page events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Messenger.Default.Register<News>(this, GoToDetailsPage);
            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);

            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<News> viewModel = (IMasterViewModel<News>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);
            ViewModelLocator.CleanMasterVM<News>(false);

            base.OnNavigatedFrom(e);
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanMasterVM<News>(true);
            base.GoBack(sender, e);
        }

        private void NewsPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            _shareContractFactory.Create(args);
        }

        #endregion

        #region Messenger methods

        private void GoToDetailsPage(News news)
        {
            Frame.Navigate(typeof(NewsDetailsPage), news);
        }

        private async void Pin(PinnableObject element)
        {
            await PinTaskHelper.CreateTile(element);
        }

        private void Share(ShareableObject conference)
        {
            try
            {
                DataTransferManager.GetForCurrentView().DataRequested -= NewsPage_DataRequested;
            }
            finally
            {
                _shareContractFactory = new ShareContractFactory((ShareableWin8Object)conference);
                DataTransferManager.GetForCurrentView().DataRequested += NewsPage_DataRequested;
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