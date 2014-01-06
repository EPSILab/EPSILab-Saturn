using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using SolarSystem.Saturn.Model.ReadersService;
using SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace SolarSystem.Saturn.View.WindowsPhone
{
    /// <summary>
    /// News page
    /// </summary>
    public partial class NewsPage
    {
        #region Constructor

        /// <summary>
        /// Constructor. Register to MVVM Light Messenger
        /// </summary>
        public NewsPage()
        {
            InitializeComponent();

            Messenger.Default.Register<PinnableObject>(this, Pin);
            Messenger.Default.Register<ShareableObject>(this, Share);
            Messenger.Default.Register<EmailableObject>(this, Email);
            Messenger.Default.Register<Uri>(this, VisitWebsite);
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the page is loaded.
        /// Load the news from the model
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
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
        private void PhoneApplicationPage_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanDetailsVM<News>(true);
        }

        #endregion

        #region Messenger methods

        /// <summary>
        /// Pin the news
        /// </summary>
        /// <param name="news">News transformed in adapted pinnable object</param>
        private void Pin(PinnableObject news)
        {
            PinTaskHelper.CreateTile(news as PinnableObjectWP);
        }

        /// <summary>
        /// Show the UI to share the news on social networks
        /// </summary>
        /// <param name="news">News transformed in adapted shareable object</param>
        private void Share(ShareableObject news)
        {
            ShareTaskHelper.Share(news);
        }

        /// <summary>
        /// Show the UI to share the news by email
        /// </summary>
        /// <param name="news">News transformed in adapted emailable object</param>
        private void Email(EmailableObject news)
        {
            EmailTaskHelper.Email(news);
        }

        /// <summary>
        /// Load the news in Internet Explorer mobile
        /// </summary>
        /// <param name="uri">Link of the news</param>
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