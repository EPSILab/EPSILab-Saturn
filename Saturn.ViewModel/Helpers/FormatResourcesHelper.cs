using SolarSystem.Saturn.ViewModel.Resources;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace SolarSystem.Saturn.ViewModel.Helpers
{
    /// <summary>
    /// Provide an easier access to the FormatResources-XX.resx file according to the user language
    /// </summary>
    static class FormatResourcesHelper
    {
        private static readonly ResourceManager ResourceManager;

        static FormatResourcesHelper()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            ResourceManager = currentCulture.Name.Contains("fr") ? FormatResources_fr.ResourceManager : FormatResources_en.ResourceManager;
        }

        public static string GetString(string name)
        {
            return ResourceManager.GetString(name);
        }
    }
}