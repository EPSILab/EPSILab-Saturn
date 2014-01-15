using Microsoft.Phone.Shell;
using SolarSystem.Saturn.View.WindowsPhone.TileFactory.Resources;

namespace SolarSystem.Saturn.View.WindowsPhone.TileFactory.Helpers
{
    /// <summary>
    /// A class helper to display a simple EPSILab toast nofication
    /// </summary>
    static class DisplayToastHelper
    {
        /// <summary>
        /// Display the toast notification
        /// </summary>
        /// <param name="content">Message to show</param>
        public static void Display(string content)
        {
            ShellToast toast = new ShellToast
            {
                Title = LibResources.AppName,
                Content = content
            };

            toast.Show();
        }
    }
}