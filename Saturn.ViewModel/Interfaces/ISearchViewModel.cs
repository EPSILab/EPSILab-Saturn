using System.Windows.Input;
using EPSILab.SolarSystem.Saturn.ViewModel.Objects;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Interfaces
{
    public interface ISearchViewModel : IMyViewModelBase
    {
        /// <summary>
        /// Get or set the search keywords
        /// </summary>
        string Keywords { get; }

        /// <summary>
        /// Get or set the results lists
        /// </summary>
        VisualMenu Results { get; }

        /// <summary>
        /// Get or set the selected item pinnable
        /// </summary>
        VisualGenericItem SelectedItem { get; set; }

        /// <summary>
        /// Launch search command
        /// </summary>
        ICommand SearchCommand { get; }

        /// <summary>
        /// Pin command
        /// </summary>
        ICommand PinCommand { get; }

        /// <summary>
        /// Show details of the clicked item command
        /// </summary>
        ICommand GoToDetailsPageCommand { get; }
    }
}