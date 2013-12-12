namespace SolarSystem.Saturn.ViewModel.Interfaces
{
    public interface IMyViewModelBase
    {
        bool IsLoading { get; }

        void Cleanup();
    }
}
