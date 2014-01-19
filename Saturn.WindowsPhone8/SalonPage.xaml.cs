using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace SolarSystem.Saturn.View.WindowsPhone
{
    /// <summary>
    /// Show page
    /// </summary>
    public partial class SalonPage
    {
        #region Constructor

        /// <summary>
        /// Constructor. Register to MVVM Light Toolkit Messenger
        /// </summary>
        public SalonPage()
        {
            InitializeComponent();

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);
            Messenger.Default.Register<Uri>(this, VisitWebsite);
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the page is loaded
        /// </summary>
        /// <param name="e">Navigation event args</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                int code = int.Parse(NavigationContext.QueryString["Id"]);
                Messenger.Default.Send(code);
            }
        }

        /// <summary>
        /// Opens the link that the user just clicked in Internet Explorer Mobile
        /// </summary>
        /// <param name="sender">WebBrowser</param>
        /// <param name="e">Navigation event arguments</param>
        private void WebBrowser_OnNavigating(object sender, NavigatingEventArgs e)
        {
            e.Cancel = true;
            WebBrowserTaskHelper.OpenBrowser(e.Uri);
        }

        /// <summary>
        /// Raised when the page is unloaded
        /// </summary>
        /// <param name="sender">Page</param>
        /// <param name="e">Event args</param>
        private void PhoneApplicationPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.DisposeDetailsVM<Salon>();
        }

        #endregion

        #region Messenger methods

        /// <summary>
        /// Pin the show
        /// </summary>
        /// <param name="salon">Show transformed in adapted pinnable object</param>
        private void Pin(PinnableObject salon)
        {
            PinTaskHelper.CreateTile(salon as PinnableObjectWP);
        }

        /// <summary>
        /// Show the UI to share the meeting on social networks
        /// </summary>
        /// <param name="salon">Show transformed in adapted shareable object</param>
        private void Share(ShareableObject salon)
        {
            ShareTaskHelper.Share(salon);
        }

        /// <summary>
        /// Load the show's informations in Internet Explorer mobile
        /// </summary>
        /// <param name="uri">Link of the meeting</param>
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