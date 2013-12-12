using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.Win8.Converters;
using SolarSystem.Saturn.Win8.Factories;
using SolarSystem.Saturn.Win8.Helpers;
using SolarSystem.Saturn.Win8.Resources;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class SalonDetailsPage
    {
        #region Attributes

        private ShareContractFactory _shareContractFactory;

        #endregion

        public SalonDetailsPage()
        {
            InitializeComponent();

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);
            Messenger.Default.Register<Uri>(this, GoWebsite);
        }

        #region Page events

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            int codeSalon = 0;

            if (navigationParameter is Salon)
            {
                Salon salon = navigationParameter as Salon;
                codeSalon = salon.Code_Salon;
            }
            else if (navigationParameter is VisualGenericItem)
            {
                VisualGenericItem salon = navigationParameter as VisualGenericItem;
                codeSalon = salon.Id;
            }

            Messenger.Default.Send(codeSalon);

            DataTransferManager.GetForCurrentView().DataRequested += SalonDetailsPage_DataRequested;
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested -= SalonDetailsPage_DataRequested;

            Messenger.Default.Unregister(this);
            ViewModelLocator.CleanDetailsVM<Salon>(true);

            base.GoBack(sender, e);
        }

        private void SalonDetailsPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (_shareContractFactory == null)
            {
                IDetailsViewModel<Salon> viewModel = (IDetailsViewModel<Salon>)DataContext;

                if (viewModel.Element != null)
                {
                    IValueConverter converter = new ToShareableConverter();
                    ShareableWin8Object conference = (ShareableWin8Object)converter.Convert(viewModel.Element, null, null, string.Empty);

                    _shareContractFactory = new ShareContractFactory(conference);
                }
            }

            if (_shareContractFactory != null)
            {
                _shareContractFactory.Create(args);
            }
        }

        #endregion

        #region Controls's events handlers

        private async void WebView_OnScriptNotify(object sender, NotifyEventArgs e)
        {
            MessageDialog messageDialog = null;

            try
            {
                string data = e.Value;

                if (data.ToLower().StartsWith("launchlink:"))
                {
                    await Launcher.LaunchUriAsync(new Uri(data.Substring("launchlink:".Length), UriKind.Absolute));
                }
            }
            catch
            {
                messageDialog = new MessageDialog(MessagesRsxAccessor.GetString("CANNOT_OPEN_WEBSITE"));
            }

            if (messageDialog != null)
            {
                await messageDialog.ShowAsync();
            }
        }

        #endregion

        #region Messenger methods

        private async void Pin(PinnableObject conference)
        {
            await PinTaskHelper.CreateTile(conference);
        }

        private void Share(ShareableObject conference)
        {
            DataTransferManager.ShowShareUI();
        }

        private async void GoWebsite(Uri uri)
        {
            await Launcher.LaunchUriAsync(uri);
        }

        #endregion
    }
}