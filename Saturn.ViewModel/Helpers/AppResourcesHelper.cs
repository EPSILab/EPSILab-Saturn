using SolarSystem.Saturn.ViewModel.Resources;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace SolarSystem.Saturn.ViewModel.Helpers
{
    /// <summary>
    /// Provide an easier access to the AppResources-XX.resx file according to the user language
    /// </summary>
    public static class AppResourcesHelper
    {
        static AppResourcesHelper()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            ResourceManager = currentCulture.Name.Contains("fr") ? AppResources_fr.ResourceManager : AppResources_en.ResourceManager;
        }

        private static readonly ResourceManager ResourceManager;
        
        public static string GetString(string name)
        {
            return ResourceManager.GetString(name);
        }
    }
}