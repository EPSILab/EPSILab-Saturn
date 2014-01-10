using GalaSoft.MvvmLight;
using SolarSystem.Saturn.ViewModel.Interfaces;

namespace SolarSystem.Saturn.ViewModel
{
    /// <summary>
    /// Base of all application's view-models.
    /// Inherits from Galasoft's ViewModelBase
    /// </summary>
    public abstract class MyViewModelBase : ViewModelBase, IMyViewModelBase
    {
        #region Attributes

        /// <summary>
        /// Determines if the page is loading data from the model
        /// </summary>
        private bool _isLoading;

        #endregion

        #region Properties

        /// <summary>
        /// Determines if the page is loading data from the model
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

        #endregion
    }
}