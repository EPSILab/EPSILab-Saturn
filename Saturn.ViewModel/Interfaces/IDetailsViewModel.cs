using System.Windows.Input;

namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    /// <summary>
    /// The interface for details view-model which shows one element
    /// </summary>
    /// <typeparam name="T">A model entity (News, Conference, Show, Member)</typeparam>
    public interface IDetailsViewModel<out T> : IMyViewModelBase
    {
        /// <summary>
        /// Displayed element
        /// </summary>
        T Element { get; }

        /// <summary>
        /// Load the element command
        /// </summary>
        ICommand LoadElementCommand { get; }

        /// <summary>
        /// Pin command
        /// </summary>
        ICommand PinCommand { get; }

        /// <summary>
        /// Share on social networks command
        /// </summary>
        ICommand ShareCommand { get; }

        /// <summary>
        /// Email command
        /// </summary>
        ICommand EmailCommand { get; }

        /// <summary>
        /// See the element on the website command
        /// </summary>
        ICommand VisitWebsiteCommand { get; }
    }
}