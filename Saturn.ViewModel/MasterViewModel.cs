using GalaSoft.MvvmLight.Command;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.ViewModel.Command;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel
{
    public class MasterViewModel<T> : MyViewModelBase, IMasterViewModel<T>
    {
        public MasterViewModel()
        {
            LoadElementsCommand = new AsyncDelegateCommand(LoadElementsAsync);
            GoToDetailsPageCommand = new RelayCommand<T>(GoToDetailsPage);
            PinCommand = new RelayCommand<PinnableObject>(Pin);
            ShareCommand = new RelayCommand<ShareableObject>(Share);
        }

        #region Public commands

        public ICommand LoadElementsCommand { get; private set; }
        public ICommand GoToDetailsPageCommand { get; private set; }
        public ICommand PinCommand { get; private set; }
        public ICommand ShareCommand { get; private set; }

        #endregion

        #region Private methods

        private async Task LoadElementsAsync()
        {
            IsLoading = true;

            IList<T> elements = await _model.GetAsync();
            Elements = new ObservableCollection<T>(elements);

            IsLoading = false;
        }

        private void GoToDetailsPage(T element)
        {
            MessengerInstance.Send(element);
        }

        private void Pin(PinnableObject element)
        {
            MessengerInstance.Send(element);
        }

        private void Share(ShareableObject element)
        {
            MessengerInstance.Send(element);
        }

        #endregion

        #region Public properties

        public ObservableCollection<T> Elements
        {
            get { return _elements; }
            protected set
            {
                _elements = value;
                RaisePropertyChanged();
            }
        }

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

        #region Private attributes

        private ObservableCollection<T> _elements;
        private T _selectedItem;

        #endregion

        #region Access to Model

        private readonly IReadable<T> _model;

        #endregion
    }
}