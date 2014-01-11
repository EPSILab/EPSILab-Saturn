using GalaSoft.MvvmLight.Messaging;
using SolarSystem.Saturn.DataAccess.Webservice;
using SolarSystem.Saturn.ViewModel;
using SolarSystem.Saturn.ViewModel.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace SolarSystem.Saturn.Win8
{
    public sealed partial class MembresPage
    {
        public MembresPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        #region Page events

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Messenger.Default.Register<Membre>(this, GoToDetailsPage);

            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<Membre> viewModel = (IMasterViewModel<Membre>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);
            ViewModelLocator.CleanMasterVM<Projet>(false);

            base.OnNavigatedFrom(e);
        }

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            ViewModelLocator.CleanMasterVM<Projet>(true);
            base.GoBack(sender, e);
        }

        #endregion

        #region Messenger methods

        private void GoToDetailsPage(Membre membre)
        {
            Frame.Navigate(typeof(MembreDetailsPage), membre);
        }

        #endregion
    }
}