using System;
using Microsoft.Phone.Tasks;

namespace SolarSystem.Saturn.View.WindowsPhone.Helpers.Tasks
{
    /// <summary>
    /// A helper to open a link in Internet Explorer mobile
    /// </summary>
    static class WebBrowserTaskHelper
    {
        /// <summary>
        /// Open the link in Internet Explorer mobile
        /// </summary>
        /// <param name="uri">The link to open</param>
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
