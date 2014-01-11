using NotificationsExtensions.TileContent;
using SolarSystem.Saturn.ViewModel.Objects;
using System;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace SolarSystem.Saturn.Win8.Helpers
{
    static class PinTaskHelper
    {
        public static async Task CreateTile(PinnableObject element)
        {
            SecondaryTile tile = new SecondaryTile
            {
                TileId = element.Id,
                ShortName = element.Title,
                DisplayName = element.Title,
                Arguments = element.Id,
                TileOptions = TileOptions.ShowNameOnLogo,
                Logo = new Uri("ms-appx:///Assets/Logo.png")
            };

            if (await tile.RequestCreateAsync())
            {
                // Tile template definition
                ITileSquarePeekImageAndText04 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText04();
                squareContent.TextBodyWrap.Text = element.Content;
                squareContent.Image.Src = element.Image;
                squareContent.Image.Alt = element.Content;

                // Tile creation
                TileNotification tileNotification = squareContent.CreateNotification();

                // Send the notification
                TileUpdater tileUpdater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(element.Id);
                tileUpdater.Update(tileNotification);

            }
        }
    }
}