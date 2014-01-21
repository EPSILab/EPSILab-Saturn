using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory.Tiles;

namespace EPSILab.SolarSystem.Saturn.Windows8.Helpers
{
    /// <summary>
    /// A helper class to pin elements on the Start Screen
    /// </summary>
    static class PinHelper
    {
        /// <summary>
        /// Show the UI to pin an element
        /// </summary>
        /// <param name="element">Element to pin</param>
        public static void Pin(PinnableObject element)
        {
            CreateSecondaryTileHelper.CreateAsync(element.Id, element.Title, element.Content, element.Image);
        }
    }
}