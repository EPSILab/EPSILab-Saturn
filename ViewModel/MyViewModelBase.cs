using SolarSystem.Saturn.ViewModel.Interfaces;
using GalaSoft.MvvmLight;

namespace SolarSystem.Saturn.ViewModel
{
    public abstract class MyViewModelBase : ViewModelBase, IMyViewModelBase
    {
        public bool IsLoading
        {
            get { return _isLoading; }
            protected set
            {
                _isLoading = value;
                RaisePropertyChanged();
            }
        }

        private bool _isLoading;
    }
}