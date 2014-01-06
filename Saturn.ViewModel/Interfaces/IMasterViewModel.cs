using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    public interface IMasterViewModel<T> : IMyViewModelBase
    {
        ICommand LoadElementsCommand { get; }
        ICommand GoToDetailsPageCommand { get; }
        ICommand PinCommand { get; }
        ICommand ShareCommand { get; }

        ObservableCollection<T> Elements { get; }
        T SelectedItem { get; set; }
    }
}