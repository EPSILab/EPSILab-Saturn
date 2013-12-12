using SolarSystem.Saturn.ViewModel.Resources;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace SolarSystem.Saturn.ViewModel.Helpers
{
    static class FormatResourcesHelper
    {
        private static readonly ResourceManager _resourceManager;

        static FormatResourcesHelper()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            _resourceManager = currentCulture.Name.Contains("fr") ? FormatResources_fr.ResourceManager : FormatResources_en.ResourceManager;
        }

        public static string GetString(string name)
        {
            return _resourceManager.GetString(name);
        }
    }
}
