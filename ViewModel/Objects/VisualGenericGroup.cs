using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace SolarSystem.Saturn.ViewModel.Objects
{
    public class VisualGenericGroup : ObservableObject
    {
        #region Attributes

        private bool _isFullyLoaded = true;

        private ObservableCollection<VisualGenericItem> _items;

        private string _title;

        #endregion

        #region Properties

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<VisualGenericItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFullyLoaded
        {
            get { return _isFullyLoaded; }
            set
            {
                _isFullyLoaded = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}