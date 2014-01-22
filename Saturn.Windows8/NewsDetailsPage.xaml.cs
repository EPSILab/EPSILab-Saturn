using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Converters;
using EPSILab.SolarSystem.Saturn.Windows8.Factories;
using EPSILab.SolarSystem.Saturn.Windows8.Helpers;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// News details page
    /// </summary>
    public sealed partial class NewsDetailsPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NewsDetailsPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Attributes

        /// <summary>
        /// A helper to share the displayed news to others apps
        /// </summary>
        private ShareContractFactory _shareContractFactory;

        #endregion

        #region Page Events

        /// <summary>
        /// Raised when the user loads or resumes the page
        /// Display the news directly or load it from the model
        /// Register to the Share contract event
        /// Register to the MVVM Light Messenger
        /// </summary>
        /// <param name="navigationParameter">Passed navigation parameters</param>
        /// <param name="pageState">Contains saved informations before the page was suspended</param>
        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            // Display the news or load it from the model
            if (navigationParameter is News)
            {
                News news = navigationParameter as News;
                Messenger.Default.Send(news);
            }
            else if (navigationParameter is VisualGenericItem)
            {
                VisualGenericItem news = navigationParameter as VisualGenericItem;
                Messenger.Default.Send(news.Id);
            }

            // Register to the share contract event
            DataTransferManager.GetForCurrentView().DataRequested += NewsDetailsPage_DataRequested;

            // Register to the MVVM Light Messenger
            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);
            Messenger.Default.Register<Uri>(this, OpenWebBrowser);
        }

        /// <summary>
        /// Raised when the user clicks on the Back button.
        /// Unregister from Share mecanism and from MVVM Light Messenger
        /// Clean the view-model
        /// Load the previous page
        /// </summary>
        /// <param name="sender">Back button</param>
        /// <param name="e">Event arguments</param>
        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            DataTransferManager.GetForCurrentView().DataRequested -= NewsDetailsPage_DataRequested;

            Messenger.Default.Unregister(this);
            ViewModelLocator.DisposeDetailsVM<News>();
            
            base.GoBack(sender, e);
        }

        /// <summary>
        /// Raised when the user open the Share UI from the Charms bar
        /// </summary>
        /// <param name="sender">Programmatically initiates an exchange of content with other apps.</param>
        /// <param name="args">Share event arguments</param>
        private void NewsDetailsPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (_shareContractFactory == null)
            {
                IDetailsViewModel<News> viewModel = (IDetailsViewModel<News>)DataContext;

                if (viewModel.Element != null)
                {
                    IValueConverter converter = new ToShareableConverter();
                    ShareableWin8Object conference = (ShareableWin8Object) converter.Convert(viewModel.Element, null, null, string.Empty);

                    _shareContractFactory = new ShareContractFactory(conference);
                }
            }

            if (_shareContractFactory != null)
            {
                _shareContractFactory.DisplayShareUI(args);
            }
        }

        #endregion

        #region Controls Events

        /// <summary>
        /// Raised when the user clicks on a hyperlink on the WebView
        /// </summary>
        /// <param name="sender">WebView</param>
        /// <param name="e">Event arguments</param>
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
                messageDialog = new MessageDialog(MessagesRsxAccessor.GetString("CannotOpenWebsite"));
            }

            if (messageDialog != null)
            {
                await messageDialog.ShowAsync();
            }
        }

        #endregion

        #region Messenger methods

        /// <summary>
        /// Pin the displayed news on the Start Screen
        /// </summary>
        /// <param name="news">The news converted in a generic object</param>
        private async void Pin(PinnableObject news)
        {
            CreateSecondaryTileHelper helper = new CreateSecondaryTileHelper();
            await helper.PinAsync(news);
        }

        /// <summary>
        /// Display the Share UI for the user to share the displayed news
        /// </summary>
        /// <param name="news">The news converted in a generic object</param>
        private void Share(ShareableObject news)
        {
            DataTransferManager.ShowShareUI();
        }

        /// <summary>
        /// Open the news in the browser
        /// </summary>
        /// <param name="uri">Conference URL</param>
        private async void OpenWebBrowser(Uri uri)
        {
            await Launcher.LaunchUriAsync(uri);
        }

        #endregion
    }
}