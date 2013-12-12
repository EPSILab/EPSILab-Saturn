using SolarSystem.Saturn.ViewModel.Resources;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace SolarSystem.Saturn.ViewModel.Helpers
{
    public static class AppResourcesHelper
    {
        private static readonly ResourceManager _resourceManager;

        static AppResourcesHelper()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            _resourceManager = currentCulture.Name.Contains("fr") ? AppResources_fr.ResourceManager : AppResources_en.ResourceManager;
        }

        public static string GetString(string name)
        {
            return _resourceManager.GetString(name);
        }
    }
}
