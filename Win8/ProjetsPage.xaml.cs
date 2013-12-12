using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class ProjetsPage
    {
        public ProjetsPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        #region Page events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Messenger.Default.Register<Projet>(this, GoToDetailsPage);

            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<Projet> viewModel = (IMasterViewModel<Projet>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Messenger.Reset();
            ViewModelLocator.CleanMasterVM<Projet>(false);
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanMasterVM<Projet>(true);
            base.GoBack(sender, e);
        }

        #endregion

        #region Messenger methods

        private void GoToDetailsPage(Projet projet)
        {
            Frame.Navigate(typeof(ProjetDetailsPage), projet);
        }

        #endregion
    }
}