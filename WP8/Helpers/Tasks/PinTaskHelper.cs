using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Shell;
using SolarSystem.Saturn.ViewModel.Objects;
using SolarSystem.Saturn.WP8.Resources;

namespace SolarSystem.Saturn.WP8.Helpers.Tasks
{
    static class PinTaskHelper
    {
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