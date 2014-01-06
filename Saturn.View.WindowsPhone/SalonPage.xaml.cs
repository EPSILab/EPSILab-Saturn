using System;
using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Objects;

namespace SolarSystem.Saturn.View.WindowsPhone
{
    public partial class SalonPage
    {
        #region Constructor

        public SalonPage()
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
            ViewModelLocator.CleanDetailsVM<Salon>(true);
        }

        #endregion

        #region Messenger methods

        private void Pin(PinnableObject salon)
        {
            PinTaskHelper.CreateTile(salon as PinnableObjectWP);
        }

        private void Share(ShareableObject salon)
        {
            ShareTaskHelper.Share(salon);
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