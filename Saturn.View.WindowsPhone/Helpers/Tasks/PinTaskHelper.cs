using Microsoft.Phone.Shell;
using SolarSystem.Saturn.View.WindowsPhone.Resources;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using System.Linq;
using System.Windows;

namespace SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks
{
    /// <summary>
    /// A helper to pin an pinnable generic object
    /// </summary>
    static class PinTaskHelper
    {
        /// <summary>
        /// Create the tile and pin it to the start screen
        /// </summary>
        /// <param name="element">Element to pin</param>
        public static void CreateTile(PinnableObjectWP element)
        {
            string id = element.Id.Substring(element.Id.LastIndexOf('-') + 1);
            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault(t => t.NavigationUri.ToString().Contains(id));

            if (tile != null)
            {
                MessageBox.Show(AppResources.MSG_ALREADY_PINNED);
            }
            else
            {
                var secondaryTile = new StandardTileData
                {
                    Title = element.Title,
                    BackContent = element.Content,
                    BackgroundImage = new Uri(element.Image, UriKind.Absolute),
                    BackTitle = element.BackTitle
                };

                ShellTile.Create(element.NavigationPage, secondaryTile);
            }
        }
    }
}