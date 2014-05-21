using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml.Navigation;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// Member master page
    /// </summary>
    public sealed partial class MembersPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MembersPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Page Events

        /// <summary>
        /// Raised when the user loads the page
        /// Register to the MVVM Light Messenger
        /// </summary>
        /// <param name="e">Navigation event arguments</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Register to the MVVM Light Messenger
            Messenger.Default.Register<Member>(this, GoToDetailsPage);

            // If the user loads the page for the first time, load elements
            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<Member> viewModel = (IMasterViewModel<Member>)DataContext;

                if (App.IsInternetAvailable && viewModel.LoadElementsCommand.CanExecute(this))
                {
                    viewModel.LoadElementsCommand.Execute(this);
                }
            }
        }

        /// <summary>
        /// Raised when the user leaves the page.
        /// Unregister from the MVVM Light Messenger
        /// Clean the associated view-model if the user goes back to the Main page or Master page
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Messenger.Default.Unregister(this);

            if (e.NavigationMode == NavigationMode.Back)
                ViewModelLocator.DisposeMasterVM<Project>();
            else
                ViewModelLocator.CleanMasterVM<Project>();

            base.OnNavigatedFrom(e);
        }

        #endregion

        #region Messenger methods

        /// <summary>
        /// Show the member in the details page
        /// </summary>
        /// <param name="member">Member to display</param>
        private void GoToDetailsPage(Member member)
        {
            Frame.Navigate(typeof(MemberDetailsPage), member);
        }

        #endregion
    }
}