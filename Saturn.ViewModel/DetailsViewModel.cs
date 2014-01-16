using Autofac;
using GalaSoft.MvvmLight.Command;
using SolarSystem.Saturn.Model.Interfaces;
using SolarSystem.Saturn.ViewModel.Interfaces;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel
{
    /// <summary>
    /// The class for details view-model which shows one element
    /// </summary>
    /// <typeparam name="T">A model entity (News, Conference, Show, Member)</typeparam>
    class DetailsViewModel<T> : MyViewModelBase, IDetailsViewModel<T>
    {
        #region Constructor

        /// <summary>
        /// Constructor. Resolve dependencies, create commands and register to the MVVM Light Toolkit Messenger
        /// </summary>
        public DetailsViewModel()
        {
            // Resolve IoC dependencies
            using (ILifetimeScope scope = ViewModelLocator.Container.BeginLifetimeScope())
            {
                _model = scope.Resolve<IReadable<T>>();
            }

            // Create commandes
            LoadElementCommand = new RelayCommand<int>(LoadElementAsync);

            PinCommand = new RelayCommand<PinnableObject>(Pin);
            ShareCommand = new RelayCommand<ShareableObject>(Share);
            EmailCommand = new RelayCommand<EmailableObject>(Email);
            VisitWebsiteCommand = new RelayCommand<Uri>(VisitWebsite);

            // Register to the Messenger
            MessengerInstance.Register<int>(this, LoadElementAsync);
            MessengerInstance.Register<T>(this, SetElement);
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Displayed element
        /// </summary>
        private T _element;

        /// <summary>
        /// Access to the model
        /// </summary>
        private readonly IReadable<T> _model;

        #endregion

        #region Properties

        /// <summary>
        /// Get the displayed element
        /// </summary>
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

        #region Commands

        /// <summary>
        /// Load the element command
        /// </summary>
        public ICommand LoadElementCommand { get; private set; }

        /// <summary>
        /// Pin command
        /// </summary>
        public ICommand PinCommand { get; private set; }

        /// <summary>
        /// Share on social networks command
        /// </summary>
        public ICommand ShareCommand { get; private set; }

        /// <summary>
        /// Email command
        /// </summary>
        public ICommand EmailCommand { get; private set; }

        /// <summary>
        /// See the element on the website command
        /// </summary>
        public ICommand VisitWebsiteCommand { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Load the element from the model with its code
        /// </summary>
        /// <param name="code">Element id</param>
        private async void LoadElementAsync(int code)
        {
            IsLoading = true;

            Element = await _model.GetAsync(code);

            IsLoading = false;
        }

        /// <summary>
        /// Set directly the element
        /// </summary>
        /// <param name="element">Element</param>
        private void SetElement(T element)
        {
            Element = element;
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

        /// <summary>
        /// Inform the UI to show the email interface
        /// </summary>
        /// <param name="element"></param>
        private void Email(EmailableObject element)
        {
            MessengerInstance.Send(element);
        }

        /// <summary>
        /// Informs the UI to open element'URL on the browser
        /// </summary>
        /// <param name="uri"></param>
        private void VisitWebsite(Uri uri)
        {
            MessengerInstance.Send(uri);
        }

        #endregion
    }
}