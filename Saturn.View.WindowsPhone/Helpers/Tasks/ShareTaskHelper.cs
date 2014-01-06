using Microsoft.Phone.Tasks;
using SolarSystem.Saturn.ViewModel.Objects;

namespace SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks
{
    /// <summary>
    /// A helper to share informations on social networks
    /// </summary>
    static class ShareTaskHelper
    {
        /// <summary>
        /// Show the UI to share informations on social networks
        /// </summary>
        /// <param name="element">Contains informations to share</param>
        public static void Share(ShareableObject element)
        {
            ShareTaskBase task = new ShareLinkTask
            {
                Title = element.Title,
                Message = element.Message,
                LinkUri = element.Uri
            };

            task.Show();
        }
    }
}