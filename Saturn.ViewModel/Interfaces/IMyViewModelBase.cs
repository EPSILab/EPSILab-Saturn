namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    /// <summary>
    /// Represents the base of all application's view-models.
    /// </summary>
    public interface IMyViewModelBase
    {
        /// <summary>
        /// Determines if the page is loading data from the model
        /// </summary>
        bool IsLoading { get; }

        /// <summary>
        /// Clean up method from MVVM Light Toolkit Messenger. Allow to use it with interfaces
        /// </summary>
        void Cleanup();
    }
}