using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.Resources;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// Member details page
    /// </summary>
    public sealed partial class MembreDetailsPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MembreDetailsPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Page Events

        /// <summary>
        /// Raised when the user loads or resumes the page
        /// Get informations from the model
        /// Register to the MVVM Light Messenger
        /// </summary>
        /// <param name="navigationParameter">Passed navigation parameters</param>
        /// <param name="pageState">Contains saved informations before the page was suspended</param>
        protected override void LoadState(object navigationParameter, Dictionary<String, Object> pageState)
        {
            if (navigationParameter is Membre)
            {
                Membre membre = navigationParameter as Membre;
                Messenger.Default.Send(membre);
            }
            else if (navigationParameter is VisualGenericItem)
            {
                VisualGenericItem membre = navigationParameter as VisualGenericItem;
                Messenger.Default.Send(membre.Id);
            }

            // Register to the MVVM Light Messenger
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
            Messenger.Default.Unregister(this);
            ViewModelLocator.DisposeDetailsVM<Membre>(); 

            base.GoBack(sender, e);
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

        #region Messenger Methods

        /// <summary>
        /// Open the conference in the browser
        /// </summary>
        /// <param name="uri">Conference URL</param>
        private async void OpenWebBrowser(Uri uri)
        {
            await Launcher.LaunchUriAsync(uri);
        }

        #endregion
    }
}