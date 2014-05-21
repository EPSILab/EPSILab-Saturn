using EPSILab.SolarSystem.Saturn.ViewModel.Objects;
using NotificationsExtensions.TileContent;
using System;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace EPSILab.SolarSystem.Saturn.Windows8.Helpers
{
    /// <summary>
    /// Create a secondary tile
    /// </summary>
    public class CreateSecondaryTileHelper
    {
        /// <summary>
        /// Create the secondary tile
        /// </summary>
        /// <param name="element">Element to pin</param>
        public async Task PinAsync(PinnableObject element)
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
                squareContent.Image.Src = element.ImageUrl;
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