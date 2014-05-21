using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.WindowsPhone8.Helpers.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Navigation;

namespace EPSILab.SolarSystem.Saturn.WindowsPhone8
{
    /// <summary>
    /// Projects page
    /// </summary>
    public partial class ProjectPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the page is loaded
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
        /// Rai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            ViewModelLocator.DisposeDetailsVM<Project>();
        }

        #endregion
    }
}