using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.WP8.Helpers.Tasks;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace SolarSystem.Saturn.WP8
{
    public partial class ConferencePage
    {
        #region Constructor

        public ConferencePage()
        {
            InitializeComponent();

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);
            Messenger.Default.Register<Uri>(this, VisitWebsite);
        }

        #endregion

        #region Events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                int code = int.Parse(NavigationContext.QueryString["Id"]);
                Messenger.Default.Send(code);
            }
        }

        private void WebBrowser_OnNavigating(object sender, NavigatingEventArgs e)
        {
            e.Cancel = true;
            WebBrowserTaskHelper.OpenBrowser(e.Uri);
        }

        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanDetailsVM<Conference>(true);
        }

        #endregion

        #region Messenger methods

        private void Pin(PinnableObject news)
        {
            PinTaskHelper.CreateTile(news as PinnableObjectWP);
        }

        private void Share(ShareableObject news)
        {
            ShareTaskHelper.Share(news);
        }

        private void VisitWebsite(Uri uri)
        {
            if (uri != null)
            {
                WebBrowserTaskHelper.OpenBrowser(uri);
            }
        }

        #endregion
    }
}