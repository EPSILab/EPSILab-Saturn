using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using EPSILab.SolarSystem.Saturn.Model.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Command;
using EPSILab.SolarSystem.Saturn.ViewModel.Interfaces;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using GalaSoft.MvvmLight.Command;

namespace EPSILab.SolarSystem.Saturn.ViewModel
{
    /// <summary>
    /// The class for master view-model which shows the elements lists
    /// </summary>
    /// <typeparam name="T">A model entity (News, Conference, Show, Member)</typeparam>
    public class MasterViewModel<T> : MyViewModelBase, IMasterViewModel<T>
    {
        #region Constructor

        /// <summary>
        /// Constructor. Resolve IoC dependencies and create commands
        /// </summary>
        public MasterViewModel()
        {
            // Resolve IoC dependencies
            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
            {
                _model = scope.Resolve<IReadable<T>>();
            }

            // Create commands
            LoadElementsCommand = new AsyncDelegateCommand(LoadElementsAsync);
            GoToDetailsPageCommand = new RelayCommand<T>(GoToDetailsPage);
            PinCommand = new RelayCommand<PinnableObject>(Pin);
            ShareCommand = new RelayCommand<ShareableObject>(Share);
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Elements list
        /// </summary>
        private ObservableCollection<T> _elements;

        /// <summary>
        /// Selected item
        /// </summary>
        private T _selectedItem;

        /// <summary>
        /// Access to the model
        /// </summary>
        private readonly IReadable<T> _model;

        #endregion

        #region Properties

        /// <summary>
        /// Get the elements list
        /// </summary>
        public ObservableCollection<T> Elements
        {
            get { return _elements; }
            protected set
            {
                _elements = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the selected item
        /// </summary>
        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Get the "load elements" command
        /// </summary>
        public ICommand LoadElementsCommand { get; private set; }

        /// <summary>
        /// Get the "go to details page" command
        /// </summary>
        public ICommand GoToDetailsPageCommand { get; private set; }

        /// <summary>
        /// Get the "pin" command
        /// </summary>
        public ICommand PinCommand { get; private set; }

        /// <summary>
        /// Get the "share" command
        /// </summary>
        public ICommand ShareCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Load the elements list from the model
        /// </summary>
        private async Task LoadElementsAsync()
        {
            IsLoading = true;

            IList<T> elements = await _model.GetAsync();
            Elements = new ObservableCollection<T>(elements);

            IsLoading = false;
        }

        /// <summary>
        /// Informs the UI to show the details page in terms of the element
        /// </summary>
        /// <param name="element">Element</param>
        private void GoToDetailsPage(T element)
        {
            MessengerInstance.Send(element);
        }

        /// <summary>
        /// Inform the UI to pin the element
        /// </summary>
        /// <param name="element">Element to pin</param>
        private void Pin(PinnableObject element)
        {
            MessengerInstance.Send(element);
        }

        /// <summary>
        /// Inform the UI to show the share UI
        /// </summary>
        /// <param name="element"></param>
        private void Share(ShareableObject element)
        {
            MessengerInstance.Send(element);
        }

        #endregion
    }
}