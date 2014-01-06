using SolarSystem.Saturn.ViewModel.Objects;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    public interface ISearchViewModel : IMyViewModelBase
    {
        ICommand SearchCommand { get; }
        ICommand PinCommand { get; }
        ICommand GoToDetailsPageCommand { get; }

        string Keyword { get; }
        VisualMenu Results { get; }
        VisualGenericItem SelectedItem { get; }
    }
}