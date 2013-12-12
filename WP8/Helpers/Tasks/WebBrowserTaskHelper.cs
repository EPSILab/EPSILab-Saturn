using System;
using Microsoft.Phone.Tasks;

namespace SolarSystem.Saturn.WP8.Helpers.Tasks
{
    static class WebBrowserTaskHelper
    {
        public static void OpenBrowser(Uri uri)
        {
            WebBrowserTask task = new WebBrowserTask
                {
                    Uri = uri
                };

            task.Show();
        }
    }
}
