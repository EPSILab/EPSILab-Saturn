using System.Globalization;
using System.Resources;
using System.Threading;
using EPSILab.SolarSystem.Saturn.ViewModel.Resources;

namespace EPSILab.SolarSystem.Saturn.ViewModel.Helpers
{
    /// <summary>
    /// Provide an easier access to the FormatResources-XX.resx file according to the user language
    /// </summary>
    static class FormatResourcesHelper
    {
        /// <summary>
        /// Static constructor
        /// </summary>
        static FormatResourcesHelper()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            ResourceManager = currentCulture.Name.Contains("fr") ? FormatResources_fr.ResourceManager : FormatResources_en.ResourceManager;
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