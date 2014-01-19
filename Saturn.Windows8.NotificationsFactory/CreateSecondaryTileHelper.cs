using NotificationsExtensions.TileContent;
using System;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace EPSILab.SolarSystem.Saturn.Windows8.NotificationsFactory
{
    /// <summary>
    /// Create a secondary tile
    /// </summary>
    public static class CreateSecondaryTileHelper
    {
        /// <summary>
        /// Create the secondary tile
        /// </summary>
        /// <param name="id">Tile id</param>
        /// <param name="title">Tile title</param>
        /// <param name="content">Tile content</param>
        /// <param name="image">Tile image source</param>
        public static async void CreateAsync(string id, string title, string content, string image)
        {


            SecondaryTile tile = new SecondaryTile
            {
                TileId = id,
                ShortName = title,
                DisplayName = title,
                Arguments = id,
                TileOptions = TileOptions.ShowNameOnLogo,
                Logo = new Uri("ms-appx:///Assets/Logo.png")
            };

            if (await tile.RequestCreateAsync())
            {
                // Tile template definition
                ITileSquarePeekImageAndText04 squareContent = TileContentFactory.CreateTileSquarePeekImageAndText04();
                squareContent.TextBodyWrap.Text = content;
                squareContent.Image.Src = image;
                squareContent.Image.Alt = content;

                // Tile creation
                TileNotification tileNotification = squareContent.CreateNotification();

                // Send the notification
                TileUpdater tileUpdater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(id);
                tileUpdater.Update(tileNotification);

            }
        }
    }
}