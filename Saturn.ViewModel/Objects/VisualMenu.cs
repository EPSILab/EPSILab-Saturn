using System.Collections.ObjectModel;

namespace SolarSystem.Saturn.ViewModel.Objects
{
    /// <summary>
    /// A generic menu binded on the main page. Display groups and items
    /// </summary>
    public class VisualMenu
    {
        public ObservableCollection<VisualGenericGroup> Groups { get; set; }
    }
}