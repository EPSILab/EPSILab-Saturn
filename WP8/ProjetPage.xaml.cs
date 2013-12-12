using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.WP8.Helpers.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SolarSystem.Saturn.WP8
{
    public partial class ProjetPage
    {
        #region Constructor

        public ProjetPage()
        {
            InitializeComponent();
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

        private void ProjetPage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanDetailsVM<Projet>(true);
        }

        #endregion
    }
}