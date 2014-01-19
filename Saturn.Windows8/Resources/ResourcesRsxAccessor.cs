using Windows.ApplicationModel.Resources;

namespace EPSILab.SolarSystem.Saturn.Windows8.Resources
{
    /// <summary>
    /// Access to the default resources file
    /// </summary>
    static class ResourcesRsxAccessor
    {
        #region Static constructor

        /// <summary>
        /// Static constructor
        /// </summary>
        static ResourcesRsxAccessor()
        {
            ResourceLoader = new ResourceLoader();
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Resource file loaded
        /// </summary>
        private static readonly ResourceLoader ResourceLoader;

        #endregion

        #region Properties

        /// <summary>
        /// Get a string from the ressource file
        /// </summary>
        /// <param name="resource">Resource key</param>
        /// <returns>Resource value</returns>
        public static string GetString(string resource)
        {
            return ResourceLoader.GetString(resource);
        }

        #endregion
    }
}