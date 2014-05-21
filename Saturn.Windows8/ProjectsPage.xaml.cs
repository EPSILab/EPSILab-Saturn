using GalaSoft.MvvmLight.Messaging;
using EPSILab.SolarSystem.Saturn.Model.ReadersService;
using EPSILab.SolarSystem.Saturn.ViewModel;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using Windows.UI.Xaml.Navigation;

namespace EPSILab.SolarSystem.Saturn.Windows8
{
    /// <summary>
    /// Project master page
    /// </summary>
    public sealed partial class ProjectsPage
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// Enables Cache Mode
        /// </summary>
        public ProjectsPage()
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
            Messenger.Default.Register<Project>(this, GoToDetailsPage);

            // If the user loads the page for the first time, load elements
            if (e.NavigationMode == NavigationMode.New)
            {
                IMasterViewModel<Project> viewModel = (IMasterViewModel<Project>)DataContext;

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
        /// Show the project in the details page
        /// </summary>
        /// <param name="projet">Project to display</param>
        private void GoToDetailsPage(Project projet)
        {
            Frame.Navigate(typeof(ProjectDetailsPage), projet);
        }

        #endregion
    }
}