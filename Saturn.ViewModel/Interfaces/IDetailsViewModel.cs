using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    public interface IDetailsViewModel<out T> : IMyViewModelBase
    {
        ICommand LoadElementCommand { get; }

        ICommand PinCommand { get; }

        ICommand ShareCommand { get; }

        ICommand EmailCommand { get; }

        ICommand VisitWebsiteCommand { get; }

        T Element { get; }
    }
}