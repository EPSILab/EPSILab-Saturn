using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.Win8.Resources;
using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class ProjetDetailsPage
    {
        public ProjetDetailsPage()
        {
            InitializeComponent();
        }

        #region Page events

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            int codeConference = 0;

            if (navigationParameter is Projet)
            {
                Projet conference = navigationParameter as Projet;
                codeConference = conference.Code_Projet;
            }
            else if (navigationParameter is VisualGenericItem)
            {
                VisualGenericItem conference = navigationParameter as VisualGenericItem;
                codeConference = conference.Id;
            }

            Messenger.Default.Send(codeConference);
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this);
            ViewModelLocator.CleanDetailsVM<Projet>(true);

            base.GoBack(sender, e);
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
    }
}