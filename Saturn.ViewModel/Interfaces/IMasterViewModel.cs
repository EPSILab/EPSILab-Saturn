using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    /// <summary>
    /// The interface for master view-model which shows the elements lists. For Windows 8 only
    /// </summary>
    /// <typeparam name="T">A model entity (News, Conference, Show, Member)</typeparam>
    public interface IMasterViewModel<T> : IMyViewModelBase
    {
        /// <summary>
        /// Get the elements
        /// </summary>
        ObservableCollection<T> Elements { get; }

        /// <summary>
        /// Get or set the selected item
        /// </summary>
        T SelectedItem { get; set; }

        /// <summary>
        /// Get the "load elements" command
        /// </summary>
        ICommand LoadElementsCommand { get; }

        /// <summary>
        /// Get the "go to details page" command
        /// </summary>
        ICommand GoToDetailsPageCommand { get; }

        /// <summary>
        /// Get the "pin" command
        /// </summary>
        ICommand PinCommand { get; }

        /// <summary>
        /// Get the "share" command
        /// </summary>
        ICommand ShareCommand { get; }
    }
}