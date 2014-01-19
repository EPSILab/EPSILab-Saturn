using Windows.ApplicationModel.Resources;

namespace Saturn.View.Windows8.TileFactory.Resources
{
    /// <summary>
    /// A helper to access to the project ressources file
    /// </summary>
    static class ResourcesRsxAccessor
    {
        #region Static constructor

        /// <summary>
        /// Static constructor
        /// </summary>
        static ResourcesRsxAccessor()
        {
            const string filename = "Ressources";
            ResourceLoader = new ResourceLoader(filename);
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Access to the ressource file
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