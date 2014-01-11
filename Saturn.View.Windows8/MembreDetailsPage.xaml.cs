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
    public sealed partial class MembreDetailsPage
    {
        public MembreDetailsPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, OpenWebBrowser);
        }

        #region Page events

        protected override void LoadState(object navigationParameter, Dictionary<String, Object> pageState)
        {
            int codeMembre = 0;

            if (navigationParameter is Membre)
            {
                Membre membre = navigationParameter as Membre;
                codeMembre = membre.Code_Membre;
            }
            else if (navigationParameter is VisualGenericItem)
            {
                VisualGenericItem membre = navigationParameter as VisualGenericItem;
                codeMembre = membre.Id;
            }

            Messenger.Default.Send(codeMembre);
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanDetailsVM<Membre>(true);
            Messenger.Default.Unregister(this);

            base.GoBack(sender, e);
        }

        #endregion

        #region Messenger methods

        private async void OpenWebBrowser(Uri uri)
        {
            await Launcher.LaunchUriAsync(uri);
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