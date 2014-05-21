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
    /// Project details page
    /// </summary>
    public sealed partial class ProjectDetailsPage
    {
        #region Constructor

        public ProjectDetailsPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Page Events

        /// <summary>
        /// Raised when the user loads or resumes the page
        /// </summary>
        /// <param name="navigationParameter">Passed navigation parameters</param>
        /// <param name="pageState">Contains saved informations before the page was suspended</param>
        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            if (navigationParameter is Project)
            {
                Project projet = navigationParameter as Project;
                Messenger.Default.Send(projet);
            }
            else if (navigationParameter is VisualGenericItem)
            {
                VisualGenericItem conference = navigationParameter as VisualGenericItem;
                Messenger.Default.Send(conference.Id);
            }
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
            ViewModelLocator.DisposeDetailsVM<Project>();

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
    }
}