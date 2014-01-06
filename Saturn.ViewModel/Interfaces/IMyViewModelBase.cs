namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    /// <summary>
    /// Represents the base of all application's view-models.
    /// </summary>
    public interface IMyViewModelBase
    {
        bool IsLoading { get; }

        void Cleanup();
    }
}