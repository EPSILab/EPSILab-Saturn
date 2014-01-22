using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Objects
{
    /// <summary>
    /// A generic object to show differents groups on the main page
    /// </summary>
    public class VisualGenericGroup : ObservableObject
    {
        #region Attributes

        /// <summary>
        /// Determines if the collection is fully loaded from the webservice
        /// </summary>
        private bool _isFullyLoaded = true;

        /// <summary>
        /// Generic items list
        /// </summary>
        private ObservableCollection<VisualGenericItem> _items;

        /// <summary>
        /// List title
        /// </summary>
        private string _title;

        #endregion

        #region Properties

        /// <summary>
        /// Determines if the list is fully loaded from the webservice
        /// </summary>
        public bool IsFullyLoaded
        {
            get { return _isFullyLoaded; }
            set
            {
                _isFullyLoaded = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the list title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Get or set the generic items list
        /// </summary>
        public ObservableCollection<VisualGenericItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}