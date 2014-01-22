using GalaSoft.MvvmLight;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Interfaces
{
    /// <summary>
    /// Represents the base of all application's view-models.
    /// </summary>
    public interface IMyViewModelBase : ICleanup
    {
        /// <summary>
        /// Determines if the page is loading data from the model
        /// </summary>
        bool IsLoading { get; }
    }
}