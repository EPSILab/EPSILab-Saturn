using EPSILab.SolarSystem.Saturn.ViewModel.Resources;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Helpers
{
    /// <summary>
    /// Provide an easier access to the AppResources-XX.resx file according to the user language
    /// </summary>
    public static class AppResourcesHelper
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static AppResourcesHelper()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            ResourceManager = currentCulture.Name.Contains("fr") ? AppResources_fr.ResourceManager : AppResources_en.ResourceManager;
        }

        /// <summary>
        /// Ressource file manager
        /// </summary>
        private static readonly ResourceManager ResourceManager;
        
        /// <summary>
        /// Get a value string corresponding to a key string
        /// </summary>
        /// <param name="name">Key</param>
        /// <returns>Corresponding value</returns>
        public static string GetString(string name)
        {
            return ResourceManager.GetString(name);
        }
    }
}