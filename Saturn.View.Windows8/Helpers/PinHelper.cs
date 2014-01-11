using Saturn.View.Windows8.TileFactory;
using SolarSystem.Saturn.ViewModel.Objects;

namespace SolarSystem.Saturn.Win8.Helpers
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