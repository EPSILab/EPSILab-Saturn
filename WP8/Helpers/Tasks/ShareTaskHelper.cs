using Microsoft.Phone.Tasks;
using SolarSystem.Saturn.ViewModel.Objects;

namespace SolarSystem.Saturn.WP8.Helpers.Tasks
{
    static class ShareTaskHelper
    {
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
