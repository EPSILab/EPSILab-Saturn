using SolarSystem.Saturn.ViewModel.Objects;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    public interface IMainViewModel : IMyViewModelBase
    {
        ICommand LoadMenuCommand { get; }
        ICommand LoadMoreItemsCommand { get; }

        ICommand GoToMasterPageCommand { get; }
        ICommand GoToDetailsPageCommand { get; }
        ICommand GoToAboutPageCommand { get; }

        ICommand PinCommand { get; }
        ICommand GoToSocialPageCommand { get; }

        VisualMenu Menu { get; }
        VisualGenericItem SelectedItem { get; set; }
    }
}