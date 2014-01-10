using SolarSystem.Saturn.ViewModel.Objects;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    /// <summary>
    /// Mainpage  view-model
    /// </summary>
    public interface IMainViewModel : IMyViewModelBase
    {
        /// <summary>
        /// Get the menu
        /// </summary>
        VisualMenu Menu { get; }
        
        /// <summary>
        /// Get the selected Item
        /// </summary>
        VisualGenericItem SelectedItem { get; set; }

        /// <summary>
        /// Get the "load the menu from model" command
        /// </summary>
        ICommand LoadMenuCommand { get; }

        /// <summary>
        /// Get the "more items from model" command (for Windows Phone only)
        /// </summary>
        ICommand LoadMoreItemsCommand { get; }

        /// <summary>
        /// Get the "go to a master page" command (list page)
        /// </summary>
        ICommand GoToMasterPageCommand { get; }

        /// <summary>
        /// Get the "go to a details page" (one element page)
        /// </summary>
        ICommand GoToDetailsPageCommand { get; }

        /// <summary>
        /// Get the "go to the about page" command
        /// </summary>
        ICommand GoToAboutPageCommand { get; }

        /// <summary>
        /// Get the "pin command" (for Windows 8 only)
        /// </summary>
        ICommand PinCommand { get; }

        /// <summary>
        /// Get the "go to social pages" command (for Windows 8 only)
        /// </summary>
        ICommand GoToSocialPageCommand { get; }
    }
}