using GalaSoft.MvvmLight.Command;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel
{
    class DetailsViewModel<T> : MyViewModelBase, IDetailsViewModel<T>
    {
        protected DetailsViewModel()
        {
            MessengerInstance.Register<int>(this, LoadElementAsync);

            LoadElementCommand = new RelayCommand<int>(LoadElementAsync);

            PinCommand = new RelayCommand<PinnableObject>(Pin);
            ShareCommand = new RelayCommand<ShareableObject>(Share);
            EmailCommand = new RelayCommand<EmailableObject>(Email);
            VisitWebsiteCommand = new RelayCommand<Uri>(VisitWebsite);
        }

        #region Public commands

        public ICommand LoadElementCommand { get; private set; }

        public ICommand PinCommand { get; private set; }

        public ICommand ShareCommand { get; private set; }

        public ICommand EmailCommand { get; private set; }

        public ICommand VisitWebsiteCommand { get; private set; }

        #endregion

        #region Private methods

        private async void LoadElementAsync(int code)
        {
            IsLoading = true;

            Element = await _model.GetAsync(code);

            IsLoading = false;
        }

        private void Pin(PinnableObject element)
        {
            MessengerInstance.Send(element);
        }

        private void Share(ShareableObject element)
        {
            MessengerInstance.Send(element);
        }

        private void Email(EmailableObject element)
        {
            MessengerInstance.Send(element);
        }

        private void VisitWebsite(Uri uri)
        {
            MessengerInstance.Send(uri);
        }

        #endregion

        #region Public properties

        public T Element
        {
            get { return _element; }
            protected set
            {
                _element = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Private attributes

        private T _element;

        #endregion

        #region Access to Model

        private readonly IReadable<T> _model;

        #endregion
    }
}