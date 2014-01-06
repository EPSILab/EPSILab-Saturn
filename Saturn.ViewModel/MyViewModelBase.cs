using SolarSystem.Saturn.ViewModel.Interfaces;
using GalaSoft.MvvmLight;

namespace SolarSystem.Saturn.ViewModel
{
    /// <summary>
    /// Base of all application's view-models.
    /// Inherits from Galasoft's ViewModelBase
    /// </summary>
    public abstract class MyViewModelBase : ViewModelBase, IMyViewModelBase
    {
        private bool _isLoading;

        /// <summary>
        /// Determines if the page is loading data
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            protected set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }
    }
}